namespace ArticlesManager.Domain.UserCharts.Features;

using ArticlesManager.Domain.UserCharts.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUserChart
{
    public class DeleteUserChartCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteUserChartCommand(Guid userChart)
        {
            Id = userChart;
        }
    }

    public class Handler : IRequestHandler<DeleteUserChartCommand, bool>
    {
        private readonly IUserChartRepository _userChartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserChartRepository userChartRepository, IUnitOfWork unitOfWork)
        {
            _userChartRepository = userChartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserChartCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _userChartRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _userChartRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}