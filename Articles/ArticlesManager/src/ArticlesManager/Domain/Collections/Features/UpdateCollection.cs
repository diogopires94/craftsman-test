namespace ArticlesManager.Domain.Collections.Features;

using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections.Validators;
using ArticlesManager.Domain.Collections.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateCollection
{
    public class UpdateCollectionCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly CollectionForUpdateDto CollectionToUpdate;

        public UpdateCollectionCommand(Guid collection, CollectionForUpdateDto newCollectionData)
        {
            Id = collection;
            CollectionToUpdate = newCollectionData;
        }
    }

    public class Handler : IRequestHandler<UpdateCollectionCommand, bool>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork)
        {
            _collectionRepository = collectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
        {
            var collectionToUpdate = await _collectionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            collectionToUpdate.Update(request.CollectionToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}