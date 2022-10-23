namespace ArticlesManager.Domain.SubFamilies.Features;

using ArticlesManager.Domain.SubFamilies.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteSubFamily
{
    public class DeleteSubFamilyCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteSubFamilyCommand(Guid subFamily)
        {
            Id = subFamily;
        }
    }

    public class Handler : IRequestHandler<DeleteSubFamilyCommand, bool>
    {
        private readonly ISubFamilyRepository _subFamilyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISubFamilyRepository subFamilyRepository, IUnitOfWork unitOfWork)
        {
            _subFamilyRepository = subFamilyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSubFamilyCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _subFamilyRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _subFamilyRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}