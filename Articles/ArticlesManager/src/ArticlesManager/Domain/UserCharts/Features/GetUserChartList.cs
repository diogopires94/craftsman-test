namespace ArticlesManager.Domain.UserCharts.Features;

using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUserChartList
{
    public class UserChartListQuery : IRequest<PagedList<UserChartDto>>
    {
        public readonly UserChartParametersDto QueryParameters;

        public UserChartListQuery(UserChartParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<UserChartListQuery, PagedList<UserChartDto>>
    {
        private readonly IUserChartRepository _userChartRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IUserChartRepository userChartRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _userChartRepository = userChartRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UserChartDto>> Handle(UserChartListQuery request, CancellationToken cancellationToken)
        {
            var collection = _userChartRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<UserChartDto>();

            return await PagedList<UserChartDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}