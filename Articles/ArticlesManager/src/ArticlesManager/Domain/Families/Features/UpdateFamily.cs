namespace ArticlesManager.Domain.Families.Features;

using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families.Validators;
using ArticlesManager.Domain.Families.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateFamily
{
    public class UpdateFamilyCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly FamilyForUpdateDto FamilyToUpdate;

        public UpdateFamilyCommand(Guid family, FamilyForUpdateDto newFamilyData)
        {
            Id = family;
            FamilyToUpdate = newFamilyData;
        }
    }

    public class Handler : IRequestHandler<UpdateFamilyCommand, bool>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFamilyRepository familyRepository, IUnitOfWork unitOfWork)
        {
            _familyRepository = familyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
        {
            var familyToUpdate = await _familyRepository.GetById(request.Id, cancellationToken: cancellationToken);

            familyToUpdate.Update(request.FamilyToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}