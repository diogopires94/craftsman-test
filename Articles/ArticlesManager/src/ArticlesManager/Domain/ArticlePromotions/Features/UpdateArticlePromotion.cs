namespace ArticlesManager.Domain.ArticlePromotions.Features;

using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions.Validators;
using ArticlesManager.Domain.ArticlePromotions.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateArticlePromotion
{
    public class UpdateArticlePromotionCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ArticlePromotionForUpdateDto ArticlePromotionToUpdate;

        public UpdateArticlePromotionCommand(Guid articlePromotion, ArticlePromotionForUpdateDto newArticlePromotionData)
        {
            Id = articlePromotion;
            ArticlePromotionToUpdate = newArticlePromotionData;
        }
    }

    public class Handler : IRequestHandler<UpdateArticlePromotionCommand, bool>
    {
        private readonly IArticlePromotionRepository _articlePromotionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticlePromotionRepository articlePromotionRepository, IUnitOfWork unitOfWork)
        {
            _articlePromotionRepository = articlePromotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateArticlePromotionCommand request, CancellationToken cancellationToken)
        {
            var articlePromotionToUpdate = await _articlePromotionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            articlePromotionToUpdate.Update(request.ArticlePromotionToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}