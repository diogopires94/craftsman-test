namespace ArticlesManager.Domain.Barcodes.Features;

using ArticlesManager.Domain.Barcodes.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteBarcode
{
    public class DeleteBarcodeCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteBarcodeCommand(Guid barcode)
        {
            Id = barcode;
        }
    }

    public class Handler : IRequestHandler<DeleteBarcodeCommand, bool>
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IBarcodeRepository barcodeRepository, IUnitOfWork unitOfWork)
        {
            _barcodeRepository = barcodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBarcodeCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _barcodeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _barcodeRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}