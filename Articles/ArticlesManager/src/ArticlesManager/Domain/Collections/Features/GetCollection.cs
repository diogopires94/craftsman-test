namespace ArticlesManager.Domain.Collections.Features;

using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetCollection
{
    public class CollectionQuery : IRequest<CollectionDto>
    {
        public readonly Guid Id;

        public CollectionQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<CollectionQuery, CollectionDto>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public Handler(ICollectionRepository collectionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _collectionRepository = collectionRepository;
        }

        public async Task<CollectionDto> Handle(CollectionQuery request, CancellationToken cancellationToken)
        {
            var result = await _collectionRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<CollectionDto>(result);
        }
    }
}