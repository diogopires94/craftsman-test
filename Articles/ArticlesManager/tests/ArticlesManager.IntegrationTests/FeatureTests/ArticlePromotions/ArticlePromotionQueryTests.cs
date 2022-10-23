namespace ArticlesManager.IntegrationTests.FeatureTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.Domain.ArticlePromotions.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Promotion;

public class ArticlePromotionQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_articlepromotion_with_accurate_props()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotionOne);

        var fakeArticlePromotionOne = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(a => a.PromotionId, _ => fakePromotionOne.Id)
            .Generate());
        await InsertAsync(fakeArticlePromotionOne);

        // Act
        var query = new GetArticlePromotion.ArticlePromotionQuery(fakeArticlePromotionOne.Id);
        var articlePromotion = await SendAsync(query);

        // Assert
        articlePromotion.Should().BeEquivalentTo(fakeArticlePromotionOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_articlepromotion_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetArticlePromotion.ArticlePromotionQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}