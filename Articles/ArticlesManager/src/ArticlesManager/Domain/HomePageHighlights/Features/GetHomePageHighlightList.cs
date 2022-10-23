namespace ArticlesManager.Domain.HomePageHighlights.Features;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetHomePageHighlightList
{
    public class HomePageHighlightListQuery : IRequest<PagedList<HomePageHighlightDto>>
    {
        public readonly HomePageHighlightParametersDto QueryParameters;

        public HomePageHighlightListQuery(HomePageHighlightParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<HomePageHighlightListQuery, PagedList<HomePageHighlightDto>>
    {
        private readonly IHomePageHighlightRepository _homePageHighlightRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IHomePageHighlightRepository homePageHighlightRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _homePageHighlightRepository = homePageHighlightRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<HomePageHighlightDto>> Handle(HomePageHighlightListQuery request, CancellationToken cancellationToken)
        {
            var collection = _homePageHighlightRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<HomePageHighlightDto>();

            return await PagedList<HomePageHighlightDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}