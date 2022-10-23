namespace ArticlesManager.Domain.Promotions.Features;

using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetPromotion
{
    public class PromotionQuery : IRequest<PromotionDto>
    {
        public readonly Guid Id;

        public PromotionQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<PromotionQuery, PromotionDto>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public Handler(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _promotionRepository = promotionRepository;
        }

        public async Task<PromotionDto> Handle(PromotionQuery request, CancellationToken cancellationToken)
        {
            var result = await _promotionRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<PromotionDto>(result);
        }
    }
}