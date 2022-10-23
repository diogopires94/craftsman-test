namespace ArticlesManager.Domain.Brands.Features;

using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands.Validators;
using ArticlesManager.Domain.Brands.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateBrand
{
    public class UpdateBrandCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly BrandForUpdateDto BrandToUpdate;

        public UpdateBrandCommand(Guid brand, BrandForUpdateDto newBrandData)
        {
            Id = brand;
            BrandToUpdate = newBrandData;
        }
    }

    public class Handler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandToUpdate = await _brandRepository.GetById(request.Id, cancellationToken: cancellationToken);

            brandToUpdate.Update(request.BrandToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}