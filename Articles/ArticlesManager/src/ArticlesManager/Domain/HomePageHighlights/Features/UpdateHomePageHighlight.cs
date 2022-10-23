namespace ArticlesManager.Domain.HomePageHighlights.Features;

using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights.Validators;
using ArticlesManager.Domain.HomePageHighlights.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateHomePageHighlight
{
    public class UpdateHomePageHighlightCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly HomePageHighlightForUpdateDto HomePageHighlightToUpdate;

        public UpdateHomePageHighlightCommand(Guid homePageHighlight, HomePageHighlightForUpdateDto newHomePageHighlightData)
        {
            Id = homePageHighlight;
            HomePageHighlightToUpdate = newHomePageHighlightData;
        }
    }

    public class Handler : IRequestHandler<UpdateHomePageHighlightCommand, bool>
    {
        private readonly IHomePageHighlightRepository _homePageHighlightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IHomePageHighlightRepository homePageHighlightRepository, IUnitOfWork unitOfWork)
        {
            _homePageHighlightRepository = homePageHighlightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateHomePageHighlightCommand request, CancellationToken cancellationToken)
        {
            var homePageHighlightToUpdate = await _homePageHighlightRepository.GetById(request.Id, cancellationToken: cancellationToken);

            homePageHighlightToUpdate.Update(request.HomePageHighlightToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}