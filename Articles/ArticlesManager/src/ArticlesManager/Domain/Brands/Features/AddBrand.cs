namespace ArticlesManager.Domain.Brands.Features;

using ArticlesManager.Domain.Brands.Services;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddBrand
{
    public class AddBrandCommand : IRequest<BrandDto>
    {
        public readonly BrandForCreationDto BrandToAdd;

        public AddBrandCommand(BrandForCreationDto brandToAdd)
        {
            BrandToAdd = brandToAdd;
        }
    }

    public class Handler : IRequestHandler<AddBrandCommand, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandDto> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = Brand.Create(request.BrandToAdd);
            await _brandRepository.Add(brand, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var brandAdded = await _brandRepository.GetById(brand.Id, cancellationToken: cancellationToken);
            return _mapper.Map<BrandDto>(brandAdded);
        }
    }
}