namespace ArticlesManager.Domain.Barcodes.Features;

using ArticlesManager.Domain.Barcodes.Services;
using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddBarcode
{
    public class AddBarcodeCommand : IRequest<BarcodeDto>
    {
        public readonly BarcodeForCreationDto BarcodeToAdd;

        public AddBarcodeCommand(BarcodeForCreationDto barcodeToAdd)
        {
            BarcodeToAdd = barcodeToAdd;
        }
    }

    public class Handler : IRequestHandler<AddBarcodeCommand, BarcodeDto>
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IBarcodeRepository barcodeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _barcodeRepository = barcodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BarcodeDto> Handle(AddBarcodeCommand request, CancellationToken cancellationToken)
        {
            var barcode = Barcode.Create(request.BarcodeToAdd);
            await _barcodeRepository.Add(barcode, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var barcodeAdded = await _barcodeRepository.GetById(barcode.Id, cancellationToken: cancellationToken);
            return _mapper.Map<BarcodeDto>(barcodeAdded);
        }
    }
}