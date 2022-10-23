namespace ArticlesManager.Domain.SizeTableLines.Features;

using ArticlesManager.Domain.SizeTableLines.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteSizeTableLine
{
    public class DeleteSizeTableLineCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteSizeTableLineCommand(Guid sizeTableLine)
        {
            Id = sizeTableLine;
        }
    }

    public class Handler : IRequestHandler<DeleteSizeTableLineCommand, bool>
    {
        private readonly ISizeTableLineRepository _sizeTableLineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISizeTableLineRepository sizeTableLineRepository, IUnitOfWork unitOfWork)
        {
            _sizeTableLineRepository = sizeTableLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSizeTableLineCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _sizeTableLineRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _sizeTableLineRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}