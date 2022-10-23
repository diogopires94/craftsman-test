namespace ArticlesManager.Domain.Brands.Features;

using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetBrandList
{
    public class BrandListQuery : IRequest<PagedList<BrandDto>>
    {
        public readonly BrandParametersDto QueryParameters;

        public BrandListQuery(BrandParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<BrandListQuery, PagedList<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IBrandRepository brandRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<BrandDto>> Handle(BrandListQuery request, CancellationToken cancellationToken)
        {
            var collection = _brandRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<BrandDto>();

            return await PagedList<BrandDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}