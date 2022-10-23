namespace ArticlesManager.Domain.UrlFilters.Features;

using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUrlFilterList
{
    public class UrlFilterListQuery : IRequest<PagedList<UrlFilterDto>>
    {
        public readonly UrlFilterParametersDto QueryParameters;

        public UrlFilterListQuery(UrlFilterParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<UrlFilterListQuery, PagedList<UrlFilterDto>>
    {
        private readonly IUrlFilterRepository _urlFilterRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IUrlFilterRepository urlFilterRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _urlFilterRepository = urlFilterRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UrlFilterDto>> Handle(UrlFilterListQuery request, CancellationToken cancellationToken)
        {
            var collection = _urlFilterRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<UrlFilterDto>();

            return await PagedList<UrlFilterDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}