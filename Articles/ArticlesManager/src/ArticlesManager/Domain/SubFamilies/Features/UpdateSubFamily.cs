namespace ArticlesManager.Domain.SubFamilies.Features;

using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies.Validators;
using ArticlesManager.Domain.SubFamilies.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateSubFamily
{
    public class UpdateSubFamilyCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly SubFamilyForUpdateDto SubFamilyToUpdate;

        public UpdateSubFamilyCommand(Guid subFamily, SubFamilyForUpdateDto newSubFamilyData)
        {
            Id = subFamily;
            SubFamilyToUpdate = newSubFamilyData;
        }
    }

    public class Handler : IRequestHandler<UpdateSubFamilyCommand, bool>
    {
        private readonly ISubFamilyRepository _subFamilyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISubFamilyRepository subFamilyRepository, IUnitOfWork unitOfWork)
        {
            _subFamilyRepository = subFamilyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSubFamilyCommand request, CancellationToken cancellationToken)
        {
            var subFamilyToUpdate = await _subFamilyRepository.GetById(request.Id, cancellationToken: cancellationToken);

            subFamilyToUpdate.Update(request.SubFamilyToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}