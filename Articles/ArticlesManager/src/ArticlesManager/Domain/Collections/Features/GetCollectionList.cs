namespace ArticlesManager.Domain.Collections.Features;

using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetCollectionList
{
    public class CollectionListQuery : IRequest<PagedList<CollectionDto>>
    {
        public readonly CollectionParametersDto QueryParameters;

        public CollectionListQuery(CollectionParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<CollectionListQuery, PagedList<CollectionDto>>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ICollectionRepository collectionRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _collectionRepository = collectionRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<CollectionDto>> Handle(CollectionListQuery request, CancellationToken cancellationToken)
        {
            var collection = _collectionRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<CollectionDto>();

            return await PagedList<CollectionDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}