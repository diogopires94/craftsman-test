namespace ArticlesManager.Domain.Collections.Features;

using ArticlesManager.Domain.Collections.Services;
using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddCollection
{
    public class AddCollectionCommand : IRequest<CollectionDto>
    {
        public readonly CollectionForCreationDto CollectionToAdd;

        public AddCollectionCommand(CollectionForCreationDto collectionToAdd)
        {
            CollectionToAdd = collectionToAdd;
        }
    }

    public class Handler : IRequestHandler<AddCollectionCommand, CollectionDto>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _collectionRepository = collectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CollectionDto> Handle(AddCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = Collection.Create(request.CollectionToAdd);
            await _collectionRepository.Add(collection, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var collectionAdded = await _collectionRepository.GetById(collection.Id, cancellationToken: cancellationToken);
            return _mapper.Map<CollectionDto>(collectionAdded);
        }
    }
}