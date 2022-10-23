namespace ArticlesManager.IntegrationTests.FeatureTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.ArticlePromotions.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Promotion;

public class AddArticlePromotionCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_articlepromotion_to_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotionOne);

        var fakeArticlePromotionOne = new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(a => a.PromotionId, _ => fakePromotionOne.Id)
            .Generate();

        // Act
        var command = new AddArticlePromotion.AddArticlePromotionCommand(fakeArticlePromotionOne);
        var articlePromotionReturned = await SendAsync(command);
        var articlePromotionCreated = await ExecuteDbContextAsync(db => db.ArticlePromotions
            .FirstOrDefaultAsync(a => a.Id == articlePromotionReturned.Id));

        // Assert
        articlePromotionReturned.Should().BeEquivalentTo(fakeArticlePromotionOne, options =>
            options.ExcludingMissingMembers());
        articlePromotionCreated.Should().BeEquivalentTo(fakeArticlePromotionOne, options =>
            options.ExcludingMissingMembers());
    }
}