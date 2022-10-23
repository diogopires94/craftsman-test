namespace ArticlesManager.Domain.SubFamilies.Features;

using ArticlesManager.Domain.SubFamilies.Services;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddSubFamily
{
    public class AddSubFamilyCommand : IRequest<SubFamilyDto>
    {
        public readonly SubFamilyForCreationDto SubFamilyToAdd;

        public AddSubFamilyCommand(SubFamilyForCreationDto subFamilyToAdd)
        {
            SubFamilyToAdd = subFamilyToAdd;
        }
    }

    public class Handler : IRequestHandler<AddSubFamilyCommand, SubFamilyDto>
    {
        private readonly ISubFamilyRepository _subFamilyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ISubFamilyRepository subFamilyRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _subFamilyRepository = subFamilyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubFamilyDto> Handle(AddSubFamilyCommand request, CancellationToken cancellationToken)
        {
            var subFamily = SubFamily.Create(request.SubFamilyToAdd);
            await _subFamilyRepository.Add(subFamily, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var subFamilyAdded = await _subFamilyRepository.GetById(subFamily.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SubFamilyDto>(subFamilyAdded);
        }
    }
}