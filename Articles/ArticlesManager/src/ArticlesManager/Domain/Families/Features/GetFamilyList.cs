namespace ArticlesManager.Domain.Families.Features;

using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families.Services;
using ArticlesManager.Wrappers;
using SharedKernel.Exceptions;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetFamilyList
{
    public class FamilyListQuery : IRequest<PagedList<FamilyDto>>
    {
        public readonly FamilyParametersDto QueryParameters;

        public FamilyListQuery(FamilyParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<FamilyListQuery, PagedList<FamilyDto>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IFamilyRepository familyRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _familyRepository = familyRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<FamilyDto>> Handle(FamilyListQuery request, CancellationToken cancellationToken)
        {
            var collection = _familyRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<FamilyDto>();

            return await PagedList<FamilyDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}