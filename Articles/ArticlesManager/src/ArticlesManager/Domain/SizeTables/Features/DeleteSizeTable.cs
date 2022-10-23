namespace ArticlesManager.Domain.SizeTables.Features;

using ArticlesManager.Domain.SizeTables.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteSizeTable
{
    public class DeleteSizeTableCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteSizeTableCommand(Guid sizeTable)
        {
            Id = sizeTable;
        }
    }

    public class Handler : IRequestHandler<DeleteSizeTableCommand, bool>
    {
        private readonly ISizeTableRepository _sizeTableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISizeTableRepository sizeTableRepository, IUnitOfWork unitOfWork)
        {
            _sizeTableRepository = sizeTableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSizeTableCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _sizeTableRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _sizeTableRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}