namespace ArticlesManager.Domain.ArticlePromotions.Features;

using ArticlesManager.Domain.ArticlePromotions.Services;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddArticlePromotion
{
    public class AddArticlePromotionCommand : IRequest<ArticlePromotionDto>
    {
        public readonly ArticlePromotionForCreationDto ArticlePromotionToAdd;

        public AddArticlePromotionCommand(ArticlePromotionForCreationDto articlePromotionToAdd)
        {
            ArticlePromotionToAdd = articlePromotionToAdd;
        }
    }

    public class Handler : IRequestHandler<AddArticlePromotionCommand, ArticlePromotionDto>
    {
        private readonly IArticlePromotionRepository _articlePromotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IArticlePromotionRepository articlePromotionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _articlePromotionRepository = articlePromotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticlePromotionDto> Handle(AddArticlePromotionCommand request, CancellationToken cancellationToken)
        {
            var articlePromotion = ArticlePromotion.Create(request.ArticlePromotionToAdd);
            await _articlePromotionRepository.Add(articlePromotion, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var articlePromotionAdded = await _articlePromotionRepository.GetById(articlePromotion.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticlePromotionDto>(articlePromotionAdded);
        }
    }
}