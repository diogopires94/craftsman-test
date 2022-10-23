namespace ArticlesManager.Domain.ArticlePromotions.Features;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetArticlePromotionList
{
    public class ArticlePromotionListQuery : IRequest<PagedList<ArticlePromotionDto>>
    {
        public readonly ArticlePromotionParametersDto QueryParameters;

        public ArticlePromotionListQuery(ArticlePromotionParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ArticlePromotionListQuery, PagedList<ArticlePromotionDto>>
    {
        private readonly IArticlePromotionRepository _articlePromotionRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IArticlePromotionRepository articlePromotionRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _articlePromotionRepository = articlePromotionRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ArticlePromotionDto>> Handle(ArticlePromotionListQuery request, CancellationToken cancellationToken)
        {
            var collection = _articlePromotionRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ArticlePromotionDto>();

            return await PagedList<ArticlePromotionDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}