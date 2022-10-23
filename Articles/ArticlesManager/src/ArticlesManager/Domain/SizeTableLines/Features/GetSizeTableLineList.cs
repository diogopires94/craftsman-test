namespace ArticlesManager.Domain.SizeTableLines.Features;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetSizeTableLineList
{
    public class SizeTableLineListQuery : IRequest<PagedList<SizeTableLineDto>>
    {
        public readonly SizeTableLineParametersDto QueryParameters;

        public SizeTableLineListQuery(SizeTableLineParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<SizeTableLineListQuery, PagedList<SizeTableLineDto>>
    {
        private readonly ISizeTableLineRepository _sizeTableLineRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ISizeTableLineRepository sizeTableLineRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _sizeTableLineRepository = sizeTableLineRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<SizeTableLineDto>> Handle(SizeTableLineListQuery request, CancellationToken cancellationToken)
        {
            var collection = _sizeTableLineRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<SizeTableLineDto>();

            return await PagedList<SizeTableLineDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}