namespace ArticlesManager.Domain.HomePageHighlights.Features;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetHomePageHighlight
{
    public class HomePageHighlightQuery : IRequest<HomePageHighlightDto>
    {
        public readonly Guid Id;

        public HomePageHighlightQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<HomePageHighlightQuery, HomePageHighlightDto>
    {
        private readonly IHomePageHighlightRepository _homePageHighlightRepository;
        private readonly IMapper _mapper;

        public Handler(IHomePageHighlightRepository homePageHighlightRepository, IMapper mapper)
        {
            _mapper = mapper;
            _homePageHighlightRepository = homePageHighlightRepository;
        }

        public async Task<HomePageHighlightDto> Handle(HomePageHighlightQuery request, CancellationToken cancellationToken)
        {
            var result = await _homePageHighlightRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<HomePageHighlightDto>(result);
        }
    }
}