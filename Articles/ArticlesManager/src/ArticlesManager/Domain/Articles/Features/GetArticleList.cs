namespace ArticlesManager.Domain.Articles.Features;

using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetArticleList
{
    public class ArticleListQuery : IRequest<PagedList<ArticleDto>>
    {
        public readonly ArticleParametersDto QueryParameters;

        public ArticleListQuery(ArticleParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ArticleListQuery, PagedList<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IArticleRepository articleRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ArticleDto>> Handle(ArticleListQuery request, CancellationToken cancellationToken)
        {
            var collection = _articleRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ArticleDto>();

            return await PagedList<ArticleDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}