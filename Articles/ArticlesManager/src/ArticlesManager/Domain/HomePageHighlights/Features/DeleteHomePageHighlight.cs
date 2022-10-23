namespace ArticlesManager.Domain.HomePageHighlights.Features;

using ArticlesManager.Domain.HomePageHighlights.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteHomePageHighlight
{
    public class DeleteHomePageHighlightCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteHomePageHighlightCommand(Guid homePageHighlight)
        {
            Id = homePageHighlight;
        }
    }

    public class Handler : IRequestHandler<DeleteHomePageHighlightCommand, bool>
    {
        private readonly IHomePageHighlightRepository _homePageHighlightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IHomePageHighlightRepository homePageHighlightRepository, IUnitOfWork unitOfWork)
        {
            _homePageHighlightRepository = homePageHighlightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteHomePageHighlightCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _homePageHighlightRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _homePageHighlightRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}