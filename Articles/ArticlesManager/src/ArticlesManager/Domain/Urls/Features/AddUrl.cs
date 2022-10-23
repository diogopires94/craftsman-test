namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls.Services;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddUrl
{
    public class AddUrlCommand : IRequest<UrlDto>
    {
        public readonly UrlForCreationDto UrlToAdd;

        public AddUrlCommand(UrlForCreationDto urlToAdd)
        {
            UrlToAdd = urlToAdd;
        }
    }

    public class Handler : IRequestHandler<AddUrlCommand, UrlDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUrlRepository urlRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _urlRepository = urlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UrlDto> Handle(AddUrlCommand request, CancellationToken cancellationToken)
        {
            var url = Url.Create(request.UrlToAdd);
            await _urlRepository.Add(url, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var urlAdded = await _urlRepository.GetById(url.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UrlDto>(urlAdded);
        }
    }
}