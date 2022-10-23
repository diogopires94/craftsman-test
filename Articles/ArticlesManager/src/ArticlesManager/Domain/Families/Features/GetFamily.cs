namespace ArticlesManager.Domain.Families.Features;

using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetFamily
{
    public class FamilyQuery : IRequest<FamilyDto>
    {
        public readonly Guid Id;

        public FamilyQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<FamilyQuery, FamilyDto>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;

        public Handler(IFamilyRepository familyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _familyRepository = familyRepository;
        }

        public async Task<FamilyDto> Handle(FamilyQuery request, CancellationToken cancellationToken)
        {
            var result = await _familyRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FamilyDto>(result);
        }
    }
}