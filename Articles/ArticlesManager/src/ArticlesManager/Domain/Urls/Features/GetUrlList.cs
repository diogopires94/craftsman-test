namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUrlList
{
    public class UrlListQuery : IRequest<PagedList<UrlDto>>
    {
        public readonly UrlParametersDto QueryParameters;

        public UrlListQuery(UrlParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<UrlListQuery, PagedList<UrlDto>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IUrlRepository urlRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _urlRepository = urlRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UrlDto>> Handle(UrlListQuery request, CancellationToken cancellationToken)
        {
            var collection = _urlRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<UrlDto>();

            return await PagedList<UrlDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}