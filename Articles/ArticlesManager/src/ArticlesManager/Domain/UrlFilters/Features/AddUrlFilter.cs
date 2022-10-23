namespace ArticlesManager.Domain.UrlFilters.Features;

using ArticlesManager.Domain.UrlFilters.Services;
using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddUrlFilter
{
    public class AddUrlFilterCommand : IRequest<UrlFilterDto>
    {
        public readonly UrlFilterForCreationDto UrlFilterToAdd;

        public AddUrlFilterCommand(UrlFilterForCreationDto urlFilterToAdd)
        {
            UrlFilterToAdd = urlFilterToAdd;
        }
    }

    public class Handler : IRequestHandler<AddUrlFilterCommand, UrlFilterDto>
    {
        private readonly IUrlFilterRepository _urlFilterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUrlFilterRepository urlFilterRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _urlFilterRepository = urlFilterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UrlFilterDto> Handle(AddUrlFilterCommand request, CancellationToken cancellationToken)
        {
            var urlFilter = UrlFilter.Create(request.UrlFilterToAdd);
            await _urlFilterRepository.Add(urlFilter, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var urlFilterAdded = await _urlFilterRepository.GetById(urlFilter.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UrlFilterDto>(urlFilterAdded);
        }
    }
}