namespace ArticlesManager.Domain.Brands.Features;

using ArticlesManager.Domain.Brands.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteBrand
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteBrandCommand(Guid brand)
        {
            Id = brand;
        }
    }

    public class Handler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _brandRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _brandRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}