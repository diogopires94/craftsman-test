namespace ArticlesManager.Domain.Barcodes.Features;

using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetBarcodeList
{
    public class BarcodeListQuery : IRequest<PagedList<BarcodeDto>>
    {
        public readonly BarcodeParametersDto QueryParameters;

        public BarcodeListQuery(BarcodeParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<BarcodeListQuery, PagedList<BarcodeDto>>
    {
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IBarcodeRepository barcodeRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _barcodeRepository = barcodeRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<BarcodeDto>> Handle(BarcodeListQuery request, CancellationToken cancellationToken)
        {
            var collection = _barcodeRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<BarcodeDto>();

            return await PagedList<BarcodeDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}