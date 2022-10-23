namespace ArticlesManager.IntegrationTests.FeatureTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.HomePageHighlights.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class UpdateHomePageHighlightCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_homepagehighlight_in_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);

        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(h => h.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(h => h.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        var updatedHomePageHighlightDto = new FakeHomePageHighlightForUpdateDto()
            .RuleFor(h => h.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(h => h.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(h => h.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();
        await InsertAsync(fakeHomePageHighlightOne);

        var homePageHighlight = await ExecuteDbContextAsync(db => db.HomePageHighlights
            .FirstOrDefaultAsync(h => h.Id == fakeHomePageHighlightOne.Id));
        var id = homePageHighlight.Id;

        // Act
        var command = new UpdateHomePageHighlight.UpdateHomePageHighlightCommand(id, updatedHomePageHighlightDto);
        await SendAsync(command);
        var updatedHomePageHighlight = await ExecuteDbContextAsync(db => db.HomePageHighlights.FirstOrDefaultAsync(h => h.Id == id));

        // Assert
        updatedHomePageHighlight.Should().BeEquivalentTo(updatedHomePageHighlightDto, options =>
            options.ExcludingMissingMembers());
    }
}