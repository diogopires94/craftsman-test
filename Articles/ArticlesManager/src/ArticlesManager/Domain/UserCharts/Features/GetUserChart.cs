namespace ArticlesManager.Domain.UserCharts.Features;

using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetUserChart
{
    public class UserChartQuery : IRequest<UserChartDto>
    {
        public readonly Guid Id;

        public UserChartQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<UserChartQuery, UserChartDto>
    {
        private readonly IUserChartRepository _userChartRepository;
        private readonly IMapper _mapper;

        public Handler(IUserChartRepository userChartRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userChartRepository = userChartRepository;
        }

        public async Task<UserChartDto> Handle(UserChartQuery request, CancellationToken cancellationToken)
        {
            var result = await _userChartRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UserChartDto>(result);
        }
    }
}