namespace ArticlesManager.Domain.UrlsWithFilters.Features;

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
using ArticlesManager.Domain.UrlsWithFilters.Dto;
using ArticlesManager.Domain.UrlsWithFilters.Dtos;

public static class GetUrlsWithSizesAndLinesList
{
    public class UrlsWithSizesAndLinesListQuery : IRequest<UrlsWithFiltersDto>
    {
        public readonly UrlsWithFiltersParametersDto QueryParameters;

        public UrlsWithSizesAndLinesListQuery(UrlsWithFiltersParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<UrlsWithSizesAndLinesListQuery, UrlsWithFiltersDto>
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

        public async Task<UrlsWithFiltersDto> Handle(UrlsWithSizesAndLinesListQuery request, CancellationToken cancellationToken)
        {

            var articles = _dbContext.Articles.Include(r => r.Brand).Include(c => c.Family).Include(x => x.SubFamily).Include(z => z.Collection)
.Where(article => _dbContext.UrlFilters.Include(x => x.Url).Where(z => z.Url.UrlValue == request.QueryParameters.Url)
.Any(urlFilters => urlFilters.BrandId == article.BrandId || urlFilters.FamilyId == article.FamilyId || urlFilters.SubFamilyId == article.SubFamilyId || urlFilters.CollectionId == article.CollectionId));

            var barcodes = _dbContext.Barcodes.Include(x => x.Article).Include(r => r.Article.Brand).Include(c => c.Article.Family).Include(x => x.Article.SubFamily).Include(z => z.Article.Collection)
.Where(article => _dbContext.UrlFilters.Include(x => x.Url).Where(z => z.Url.UrlValue == request.QueryParameters.Url)
.Any(urlFilters => urlFilters.BrandId == article.Article.BrandId || urlFilters.FamilyId == article.Article.FamilyId || urlFilters.SubFamilyId == article.Article.SubFamilyId || urlFilters.CollectionId == article.Article.CollectionId));


            UrlsWithFiltersDto result = new UrlsWithFiltersDto();


            result.Brands = articles.Select(x => x.Brand).Distinct().Select(x => new Brand
{
    Id = x.Id.ToString(),
    Name = x.Description
}).ToList();

            result.Sizes = barcodes.Select(x=>x.Size).Distinct().Select(x => new Size 
            { US = x }).ToList();

            result.PriceFrom = articles.Min(x=>x.PriceWithPromotion).Value;

            result.PriceTo = articles.Max(x => x.PriceWithPromotion).Value;

            //result.Family = articles.Select(x => x.Family).Distinct().Select(x => new Family
            //{
            //    Id = x.Id.ToString(),
            //    Name = x.Description,
            //    SubFamilies = articles.Select(x => x.SubFamily).Distinct().Select(x => new SubFamily
            //    {
            //        Id = x.Id.ToString(),
            //        Name = x.Description
            //    }).ToList()
            //}).ToList();

            result.SubFamily = articles.Select(x => x.SubFamily).Distinct().Select(x => new SubFamily
            {
                Id = x.Id.ToString(),
                Name = x.Description
            }).ToList();

            result.IsOutletAvailable = _dbContext.Articles.Where(article => _dbContext.UrlFilters.Include(x => x.Url).Where(z => z.Url.UrlValue == request.QueryParameters.Url).Any())
            .Where(x => x.IsOutlet).Any();

            return result;
        }
    }
}