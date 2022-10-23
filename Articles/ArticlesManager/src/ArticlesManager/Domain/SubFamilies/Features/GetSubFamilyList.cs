namespace ArticlesManager.Domain.SubFamilies.Features;

using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetSubFamilyList
{
    public class SubFamilyListQuery : IRequest<PagedList<SubFamilyDto>>
    {
        public readonly SubFamilyParametersDto QueryParameters;

        public SubFamilyListQuery(SubFamilyParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<SubFamilyListQuery, PagedList<SubFamilyDto>>
    {
        private readonly ISubFamilyRepository _subFamilyRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ISubFamilyRepository subFamilyRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _subFamilyRepository = subFamilyRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<SubFamilyDto>> Handle(SubFamilyListQuery request, CancellationToken cancellationToken)
        {
            var collection = _subFamilyRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<SubFamilyDto>();

            return await PagedList<SubFamilyDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}