namespace ArticlesManager.Domain.ArticlePromotions.Mappings;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions;
using Mapster;

public class ArticlePromotionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticlePromotionDto, ArticlePromotion>()
            .TwoWays();
        config.NewConfig<ArticlePromotionForCreationDto, ArticlePromotion>()
            .TwoWays();
        config.NewConfig<ArticlePromotionForUpdateDto, ArticlePromotion>()
            .TwoWays();
    }
}