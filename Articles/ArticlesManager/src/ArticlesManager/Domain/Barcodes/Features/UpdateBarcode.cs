namespace ArticlesManager.Domain.Barcodes.Features;

using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Validators;
using ArticlesManager.Domain.Barcodes.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateBarcode
{
    public class UpdateBarcodeCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly BarcodeForUpdateDto BarcodeToUpdate;

        public UpdateBarcodeCommand(Guid barcode, BarcodeForUpdateDto newBarcodeData)
        {
            Id = barcode;
            BarcodeToUpdate = newBarcodeData;
        }
    }

    public class Handler : IRequestHandler<UpdateBarcodeCommand, bool>
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IBarcodeRepository barcodeRepository, IUnitOfWork unitOfWork)
        {
            _barcodeRepository = barcodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateBarcodeCommand request, CancellationToken cancellationToken)
        {
            var barcodeToUpdate = await _barcodeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            barcodeToUpdate.Update(request.BarcodeToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}