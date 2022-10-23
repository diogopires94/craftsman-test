namespace ArticlesManager.IntegrationTests.FeatureTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.Domain.ArticlePromotions.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticlePromotions.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Promotion;

public class UpdateArticlePromotionCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_articlepromotion_in_db()
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
        var updatedArticlePromotionDto = new FakeArticlePromotionForUpdateDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(a => a.PromotionId, _ => fakePromotionOne.Id)
            .Generate();
        await InsertAsync(fakeArticlePromotionOne);

        var articlePromotion = await ExecuteDbContextAsync(db => db.ArticlePromotions
            .FirstOrDefaultAsync(a => a.Id == fakeArticlePromotionOne.Id));
        var id = articlePromotion.Id;

        // Act
        var command = new UpdateArticlePromotion.UpdateArticlePromotionCommand(id, updatedArticlePromotionDto);
        await SendAsync(command);
        var updatedArticlePromotion = await ExecuteDbContextAsync(db => db.ArticlePromotions.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedArticlePromotion.Should().BeEquivalentTo(updatedArticlePromotionDto, options =>
            options.ExcludingMissingMembers());
    }
}