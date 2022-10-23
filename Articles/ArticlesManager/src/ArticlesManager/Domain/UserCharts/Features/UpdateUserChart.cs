namespace ArticlesManager.Domain.UserCharts.Features;

using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts.Validators;
using ArticlesManager.Domain.UserCharts.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateUserChart
{
    public class UpdateUserChartCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UserChartForUpdateDto UserChartToUpdate;

        public UpdateUserChartCommand(Guid userChart, UserChartForUpdateDto newUserChartData)
        {
            Id = userChart;
            UserChartToUpdate = newUserChartData;
        }
    }

    public class Handler : IRequestHandler<UpdateUserChartCommand, bool>
    {
        private readonly IUserChartRepository _userChartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserChartRepository userChartRepository, IUnitOfWork unitOfWork)
        {
            _userChartRepository = userChartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserChartCommand request, CancellationToken cancellationToken)
        {
            var userChartToUpdate = await _userChartRepository.GetById(request.Id, cancellationToken: cancellationToken);

            userChartToUpdate.Update(request.UserChartToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}