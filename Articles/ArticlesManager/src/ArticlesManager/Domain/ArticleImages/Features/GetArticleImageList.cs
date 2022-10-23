namespace ArticlesManager.Domain.ArticleImages.Features;

using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetArticleImageList
{
    public class ArticleImageListQuery : IRequest<PagedList<ArticleImageDto>>
    {
        public readonly ArticleImageParametersDto QueryParameters;

        public ArticleImageListQuery(ArticleImageParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ArticleImageListQuery, PagedList<ArticleImageDto>>
    {
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IArticleImageRepository articleImageRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _articleImageRepository = articleImageRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ArticleImageDto>> Handle(ArticleImageListQuery request, CancellationToken cancellationToken)
        {
            var collection = _articleImageRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ArticleImageDto>();

            return await PagedList<ArticleImageDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}