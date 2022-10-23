namespace ArticlesManager.Domain.Barcodes.Features;

using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetBarcode
{
    public class BarcodeQuery : IRequest<BarcodeDto>
    {
        public readonly Guid Id;

        public BarcodeQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<BarcodeQuery, BarcodeDto>
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly IMapper _mapper;

        public Handler(IBarcodeRepository barcodeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _barcodeRepository = barcodeRepository;
        }

        public async Task<BarcodeDto> Handle(BarcodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _barcodeRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<BarcodeDto>(result);
        }
    }
}