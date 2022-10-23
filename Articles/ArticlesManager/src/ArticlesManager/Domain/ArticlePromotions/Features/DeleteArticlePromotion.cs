namespace ArticlesManager.Domain.ArticlePromotions.Features;

using ArticlesManager.Domain.ArticlePromotions.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteArticlePromotion
{
    public class DeleteArticlePromotionCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteArticlePromotionCommand(Guid articlePromotion)
        {
            Id = articlePromotion;
        }
    }

    public class Handler : IRequestHandler<DeleteArticlePromotionCommand, bool>
    {
        private readonly IArticlePromotionRepository _articlePromotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticlePromotionRepository articlePromotionRepository, IUnitOfWork unitOfWork)
        {
            _articlePromotionRepository = articlePromotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteArticlePromotionCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _articlePromotionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _articlePromotionRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}