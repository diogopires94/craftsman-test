namespace ArticlesManager.Domain.UserCharts.Features;

using ArticlesManager.Domain.UserCharts.Services;
using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddUserChart
{
    public class AddUserChartCommand : IRequest<UserChartDto>
    {
        public readonly UserChartForCreationDto UserChartToAdd;

        public AddUserChartCommand(UserChartForCreationDto userChartToAdd)
        {
            UserChartToAdd = userChartToAdd;
        }
    }

    public class Handler : IRequestHandler<AddUserChartCommand, UserChartDto>
    {
        private readonly IUserChartRepository _userChartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUserChartRepository userChartRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _userChartRepository = userChartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserChartDto> Handle(AddUserChartCommand request, CancellationToken cancellationToken)
        {
            var userChart = UserChart.Create(request.UserChartToAdd);
            await _userChartRepository.Add(userChart, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var userChartAdded = await _userChartRepository.GetById(userChart.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UserChartDto>(userChartAdded);
        }
    }
}