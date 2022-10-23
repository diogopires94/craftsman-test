namespace ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;

using AutoBogus;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;

public class FakeArticlePromotion
{
    public static ArticlePromotion Generate(ArticlePromotionForCreationDto articlePromotionForCreationDto)
    {
        return ArticlePromotion.Create(articlePromotionForCreationDto);
    }

    public static ArticlePromotion Generate()
    {
        return ArticlePromotion.Create(new FakeArticlePromotionForCreationDto().Generate());
    }
}