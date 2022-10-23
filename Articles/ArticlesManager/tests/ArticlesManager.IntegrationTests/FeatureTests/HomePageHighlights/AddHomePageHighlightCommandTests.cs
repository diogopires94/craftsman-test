namespace ArticlesManager.IntegrationTests.FeatureTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.HomePageHighlights.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class AddHomePageHighlightCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_homepagehighlight_to_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);

        var fakeHomePageHighlightOne = new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(h => h.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(h => h.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();

        // Act
        var command = new AddHomePageHighlight.AddHomePageHighlightCommand(fakeHomePageHighlightOne);
        var homePageHighlightReturned = await SendAsync(command);
        var homePageHighlightCreated = await ExecuteDbContextAsync(db => db.HomePageHighlights
            .FirstOrDefaultAsync(h => h.Id == homePageHighlightReturned.Id));

        // Assert
        homePageHighlightReturned.Should().BeEquivalentTo(fakeHomePageHighlightOne, options =>
            options.ExcludingMissingMembers());
        homePageHighlightCreated.Should().BeEquivalentTo(fakeHomePageHighlightOne, options =>
            options.ExcludingMissingMembers());
    }
}