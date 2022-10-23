namespace ArticlesManager.Domain.HomePageHighlights.Features;

using ArticlesManager.Domain.HomePageHighlights.Services;
using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddHomePageHighlight
{
    public class AddHomePageHighlightCommand : IRequest<HomePageHighlightDto>
    {
        public readonly HomePageHighlightForCreationDto HomePageHighlightToAdd;

        public AddHomePageHighlightCommand(HomePageHighlightForCreationDto homePageHighlightToAdd)
        {
            HomePageHighlightToAdd = homePageHighlightToAdd;
        }
    }

    public class Handler : IRequestHandler<AddHomePageHighlightCommand, HomePageHighlightDto>
    {
        private readonly IHomePageHighlightRepository _homePageHighlightRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IHomePageHighlightRepository homePageHighlightRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _homePageHighlightRepository = homePageHighlightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HomePageHighlightDto> Handle(AddHomePageHighlightCommand request, CancellationToken cancellationToken)
        {
            var homePageHighlight = HomePageHighlight.Create(request.HomePageHighlightToAdd);
            await _homePageHighlightRepository.Add(homePageHighlight, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var homePageHighlightAdded = await _homePageHighlightRepository.GetById(homePageHighlight.Id, cancellationToken: cancellationToken);
            return _mapper.Map<HomePageHighlightDto>(homePageHighlightAdded);
        }
    }
}