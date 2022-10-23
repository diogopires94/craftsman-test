namespace ArticlesManager.Domain.SubFamilies.Features;

using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetSubFamily
{
    public class SubFamilyQuery : IRequest<SubFamilyDto>
    {
        public readonly Guid Id;

        public SubFamilyQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<SubFamilyQuery, SubFamilyDto>
    {
        private readonly ISubFamilyRepository _subFamilyRepository;
        private readonly IMapper _mapper;

        public Handler(ISubFamilyRepository subFamilyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subFamilyRepository = subFamilyRepository;
        }

        public async Task<SubFamilyDto> Handle(SubFamilyQuery request, CancellationToken cancellationToken)
        {
            var result = await _subFamilyRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SubFamilyDto>(result);
        }
    }
}