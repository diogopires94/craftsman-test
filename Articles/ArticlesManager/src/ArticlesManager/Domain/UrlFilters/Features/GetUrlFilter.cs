namespace ArticlesManager.Domain.UrlFilters.Features;

using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetUrlFilter
{
    public class UrlFilterQuery : IRequest<UrlFilterDto>
    {
        public readonly Guid Id;

        public UrlFilterQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<UrlFilterQuery, UrlFilterDto>
    {
        private readonly IUrlFilterRepository _urlFilterRepository;
        private readonly IMapper _mapper;

        public Handler(IUrlFilterRepository urlFilterRepository, IMapper mapper)
        {
            _mapper = mapper;
            _urlFilterRepository = urlFilterRepository;
        }

        public async Task<UrlFilterDto> Handle(UrlFilterQuery request, CancellationToken cancellationToken)
        {
            var result = await _urlFilterRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UrlFilterDto>(result);
        }
    }
}