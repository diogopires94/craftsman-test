namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetUrl
{
    public class UrlQuery : IRequest<UrlDto>
    {
        public readonly Guid Id;

        public UrlQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<UrlQuery, UrlDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public Handler(IUrlRepository urlRepository, IMapper mapper)
        {
            _mapper = mapper;
            _urlRepository = urlRepository;
        }

        public async Task<UrlDto> Handle(UrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _urlRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UrlDto>(result);
        }
    }
}