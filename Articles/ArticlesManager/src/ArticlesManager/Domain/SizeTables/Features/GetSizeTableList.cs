namespace ArticlesManager.Domain.SizeTables.Features;

using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetSizeTableList
{
    public class SizeTableListQuery : IRequest<PagedList<SizeTableDto>>
    {
        public readonly SizeTableParametersDto QueryParameters;

        public SizeTableListQuery(SizeTableParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<SizeTableListQuery, PagedList<SizeTableDto>>
    {
        private readonly ISizeTableRepository _sizeTableRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ISizeTableRepository sizeTableRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _sizeTableRepository = sizeTableRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<SizeTableDto>> Handle(SizeTableListQuery request, CancellationToken cancellationToken)
        {
            var collection = _sizeTableRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<SizeTableDto>();

            return await PagedList<SizeTableDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}