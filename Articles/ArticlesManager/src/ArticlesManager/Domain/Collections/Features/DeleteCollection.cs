namespace ArticlesManager.Domain.Collections.Features;

using ArticlesManager.Domain.Collections.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteCollection
{
    public class DeleteCollectionCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteCollectionCommand(Guid collection)
        {
            Id = collection;
        }
    }

    public class Handler : IRequestHandler<DeleteCollectionCommand, bool>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICollectionRepository collectionRepository, IUnitOfWork unitOfWork)
        {
            _collectionRepository = collectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _collectionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _collectionRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}