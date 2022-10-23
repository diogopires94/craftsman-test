namespace ArticlesManager.Domain.Promotions.Features;

using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetPromotionList
{
    public class PromotionListQuery : IRequest<PagedList<PromotionDto>>
    {
        public readonly PromotionParametersDto QueryParameters;

        public PromotionListQuery(PromotionParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<PromotionListQuery, PagedList<PromotionDto>>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IPromotionRepository promotionRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _promotionRepository = promotionRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<PromotionDto>> Handle(PromotionListQuery request, CancellationToken cancellationToken)
        {
            var collection = _promotionRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<PromotionDto>();

            return await PagedList<PromotionDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}