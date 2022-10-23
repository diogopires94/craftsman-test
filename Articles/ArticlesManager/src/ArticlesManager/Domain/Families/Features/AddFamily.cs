namespace ArticlesManager.Domain.Families.Features;

using ArticlesManager.Domain.Families.Services;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddFamily
{
    public class AddFamilyCommand : IRequest<FamilyDto>
    {
        public readonly FamilyForCreationDto FamilyToAdd;

        public AddFamilyCommand(FamilyForCreationDto familyToAdd)
        {
            FamilyToAdd = familyToAdd;
        }
    }

    public class Handler : IRequestHandler<AddFamilyCommand, FamilyDto>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IFamilyRepository familyRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _familyRepository = familyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FamilyDto> Handle(AddFamilyCommand request, CancellationToken cancellationToken)
        {
            var family = Family.Create(request.FamilyToAdd);
            await _familyRepository.Add(family, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var familyAdded = await _familyRepository.GetById(family.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FamilyDto>(familyAdded);
        }
    }
}