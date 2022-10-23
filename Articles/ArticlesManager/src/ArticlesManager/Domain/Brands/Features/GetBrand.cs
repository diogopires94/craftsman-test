namespace ArticlesManager.Domain.Brands.Features;

using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetBrand
{
    public class BrandQuery : IRequest<BrandDto>
    {
        public readonly Guid Id;

        public BrandQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<BrandQuery, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public Handler(IBrandRepository brandRepository, IMapper mapper)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<BrandDto> Handle(BrandQuery request, CancellationToken cancellationToken)
        {
            var result = await _brandRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<BrandDto>(result);
        }
    }
}