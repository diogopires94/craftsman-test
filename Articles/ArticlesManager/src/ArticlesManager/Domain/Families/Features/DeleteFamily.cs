namespace ArticlesManager.Domain.Families.Features;

using ArticlesManager.Domain.Families.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteFamily
{
    public class DeleteFamilyCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteFamilyCommand(Guid family)
        {
            Id = family;
        }
    }

    public class Handler : IRequestHandler<DeleteFamilyCommand, bool>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFamilyRepository familyRepository, IUnitOfWork unitOfWork)
        {
            _familyRepository = familyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _familyRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _familyRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}