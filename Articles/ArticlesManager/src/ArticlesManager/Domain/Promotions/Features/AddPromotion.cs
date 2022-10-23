namespace ArticlesManager.Domain.Promotions.Features;

using ArticlesManager.Domain.Promotions.Services;
using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddPromotion
{
    public class AddPromotionCommand : IRequest<PromotionDto>
    {
        public readonly PromotionForCreationDto PromotionToAdd;

        public AddPromotionCommand(PromotionForCreationDto promotionToAdd)
        {
            PromotionToAdd = promotionToAdd;
        }
    }

    public class Handler : IRequestHandler<AddPromotionCommand, PromotionDto>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PromotionDto> Handle(AddPromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = Promotion.Create(request.PromotionToAdd);
            await _promotionRepository.Add(promotion, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var promotionAdded = await _promotionRepository.GetById(promotion.Id, cancellationToken: cancellationToken);
            return _mapper.Map<PromotionDto>(promotionAdded);
        }
    }
}