namespace ArticlesManager.SharedTestHelpers.Fakes.Promotion;

using AutoBogus;
using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.Dtos;

public class FakePromotion
{
    public static Promotion Generate(PromotionForCreationDto promotionForCreationDto)
    {
        return Promotion.Create(promotionForCreationDto);
    }

    public static Promotion Generate()
    {
        return Promotion.Create(new FakePromotionForCreationDto().Generate());
    }
}