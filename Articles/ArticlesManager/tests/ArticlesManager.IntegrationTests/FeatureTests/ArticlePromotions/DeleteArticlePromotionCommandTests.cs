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

public class DeleteArticlePromotionCommandTests : TestBase
{
    [Test]
    public async Task can_delete_articlepromotion_from_db()
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
        var articlePromotion = await ExecuteDbContextAsync(db => db.ArticlePromotions
            .FirstOrDefaultAsync(a => a.Id == fakeArticlePromotionOne.Id));

        // Act
        var command = new DeleteArticlePromotion.DeleteArticlePromotionCommand(articlePromotion.Id);
        await SendAsync(command);
        var articlePromotionResponse = await ExecuteDbContextAsync(db => db.ArticlePromotions.CountAsync(a => a.Id == articlePromotion.Id));

        // Assert
        articlePromotionResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_articlepromotion_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteArticlePromotion.DeleteArticlePromotionCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_articlepromotion_from_db()
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
        var articlePromotion = await ExecuteDbContextAsync(db => db.ArticlePromotions
            .FirstOrDefaultAsync(a => a.Id == fakeArticlePromotionOne.Id));

        // Act
        var command = new DeleteArticlePromotion.DeleteArticlePromotionCommand(articlePromotion.Id);
        await SendAsync(command);
        var deletedArticlePromotion = await ExecuteDbContextAsync(db => db.ArticlePromotions
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == articlePromotion.Id));

        // Assert
        deletedArticlePromotion?.IsDeleted.Should().BeTrue();
    }
}