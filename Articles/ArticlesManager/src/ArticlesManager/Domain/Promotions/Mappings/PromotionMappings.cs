namespace ArticlesManager.Domain.Promotions.Mappings;

using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions;
using Mapster;

public class PromotionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PromotionDto, Promotion>()
            .TwoWays();
        config.NewConfig<PromotionForCreationDto, Promotion>()
            .TwoWays();
        config.NewConfig<PromotionForUpdateDto, Promotion>()
            .TwoWays();
    }
}