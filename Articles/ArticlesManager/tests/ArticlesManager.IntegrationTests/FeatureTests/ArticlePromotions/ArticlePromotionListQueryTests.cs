namespace ArticlesManager.IntegrationTests.FeatureTests.ArticlePromotions;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticlePromotions.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Promotion;

public class ArticlePromotionListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_articlepromotion_list()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    await InsertAsync(fakeArticleOne, fakeArticleTwo);

        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
    var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
    await InsertAsync(fakePromotionOne, fakePromotionTwo);

        var fakeArticlePromotionOne = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(a => a.PromotionId, _ => fakePromotionOne.Id)
            .Generate());
        var fakeArticlePromotionTwo = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleTwo.Id)
            
            .RuleFor(a => a.PromotionId, _ => fakePromotionTwo.Id)
            .Generate());
        var queryParameters = new ArticlePromotionParametersDto();

        await InsertAsync(fakeArticlePromotionOne, fakeArticlePromotionTwo);

        // Act
        var query = new GetArticlePromotionList.ArticlePromotionListQuery(queryParameters);
        var articlePromotions = await SendAsync(query);

        // Assert
        articlePromotions.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}