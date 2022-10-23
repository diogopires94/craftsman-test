namespace ArticlesManager.Domain.Promotions.Features;

using ArticlesManager.Domain.Promotions.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeletePromotion
{
    public class DeletePromotionCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeletePromotionCommand(Guid promotion)
        {
            Id = promotion;
        }
    }

    public class Handler : IRequestHandler<DeletePromotionCommand, bool>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _promotionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _promotionRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}