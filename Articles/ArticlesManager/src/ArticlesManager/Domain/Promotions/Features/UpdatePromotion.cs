namespace ArticlesManager.Domain.Promotions.Features;

using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions.Validators;
using ArticlesManager.Domain.Promotions.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdatePromotion
{
    public class UpdatePromotionCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly PromotionForUpdateDto PromotionToUpdate;

        public UpdatePromotionCommand(Guid promotion, PromotionForUpdateDto newPromotionData)
        {
            Id = promotion;
            PromotionToUpdate = newPromotionData;
        }
    }

    public class Handler : IRequestHandler<UpdatePromotionCommand, bool>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotionToUpdate = await _promotionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            promotionToUpdate.Update(request.PromotionToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}