namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Databases;
using ArticlesManager.Domain.UrlFilters.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ArticlesManager.Domain.UrlsWithProducts.Dtos;
using ArticlesManager.Domain.UrlsWithProducts.Dto;

public static class GetUrlsWithProductsList
{
    public class UrlsWithProductsListQuery : IRequest<PagedList<UrlsWithProductsDto>>
    {
        public readonly UrlsWithProductsParametersDto QueryParameters;

        public UrlsWithProductsListQuery(UrlsWithProductsParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<UrlsWithProductsListQuery, PagedList<UrlsWithProductsDto>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        private readonly ArticlesDbContext _dbContext;


        public Handler(IUrlRepository urlRepository, IMapper mapper, SieveProcessor sieveProcessor, ArticlesDbContext dbContext)
        {
            _mapper = mapper;
            _urlRepository = urlRepository;
            _sieveProcessor = sieveProcessor;
            _dbContext = dbContext;
        }

        public async Task<PagedList<UrlsWithProductsDto>> Handle(UrlsWithProductsListQuery request, CancellationToken cancellationToken)
        {
            var articles = _dbContext.Articles.Include(r => r.Brand).Include(c => c.Family).Include(x => x.SubFamily).Include(z => z.Collection)
                .Where(article => _dbContext.UrlFilters.Include(x => x.Url).Where(z => z.Url.UrlValue == request.QueryParameters.Url).
                        Any(urlFilters => urlFilters.BrandId == article.BrandId || urlFilters.FamilyId == article.FamilyId || urlFilters.SubFamilyId == article.SubFamilyId || urlFilters.CollectionId == article.CollectionId))
                .Select(x => new UrlsWithProductsDto
                {
                    article_sku = x.SKU,
                    article_description = x.Description,
                    article_price_with_promotion = x.PriceWithPromotion,
                    article_main_article_image_url = x.MainArticleImageUrl,
                    article_meta_name = x.MetaName,
                    article_meta_description = x.MetaDescription,
                    article_is_low_stock = x.IsLowStock,
                    article_is_outlet = x.IsOutlet,
                    brand_description = x.Brand.Description,
                    family_description = x.Family.Description,
                    sub_family_description = x.SubFamily.Description,
                    collection_description = x.Collection.Description
                }).AsQueryable();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "article_price_with_promotion",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, articles);
            var dtoCollection = appliedCollection
                .ProjectToType<UrlsWithProductsDto>();

            return await PagedList<UrlsWithProductsDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);

        }
    }
}