namespace ArticlesManager.Domain.ArticlePromotions.Features;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetArticlePromotion
{
    public class ArticlePromotionQuery : IRequest<ArticlePromotionDto>
    {
        public readonly Guid Id;

        public ArticlePromotionQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ArticlePromotionQuery, ArticlePromotionDto>
    {
        private readonly IArticlePromotionRepository _articlePromotionRepository;
        private readonly IMapper _mapper;

        public Handler(IArticlePromotionRepository articlePromotionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _articlePromotionRepository = articlePromotionRepository;
        }

        public async Task<ArticlePromotionDto> Handle(ArticlePromotionQuery request, CancellationToken cancellationToken)
        {
            var result = await _articlePromotionRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticlePromotionDto>(result);
        }
    }
}