namespace ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;

using AutoBogus;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakeArticlePromotionDto : AutoFaker<ArticlePromotionDto>
{
    public FakeArticlePromotionDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(a => a.ExampleIntProperty, a => a.Random.Number(50, 100000));
        //RuleFor(a => a.ExampleDateProperty, a => a.Date.Past());
    }
}