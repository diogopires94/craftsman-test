namespace ArticlesManager.IntegrationTests.FeatureTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.Domain.HomePageHighlights.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class DeleteHomePageHighlightCommandTests : TestBase
{
    [Test]
    public async Task can_delete_homepagehighlight_from_db()
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
        await InsertAsync(fakeHomePageHighlightOne);
        var homePageHighlight = await ExecuteDbContextAsync(db => db.HomePageHighlights
            .FirstOrDefaultAsync(h => h.Id == fakeHomePageHighlightOne.Id));

        // Act
        var command = new DeleteHomePageHighlight.DeleteHomePageHighlightCommand(homePageHighlight.Id);
        await SendAsync(command);
        var homePageHighlightResponse = await ExecuteDbContextAsync(db => db.HomePageHighlights.CountAsync(h => h.Id == homePageHighlight.Id));

        // Assert
        homePageHighlightResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_homepagehighlight_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteHomePageHighlight.DeleteHomePageHighlightCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_homepagehighlight_from_db()
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
        await InsertAsync(fakeHomePageHighlightOne);
        var homePageHighlight = await ExecuteDbContextAsync(db => db.HomePageHighlights
            .FirstOrDefaultAsync(h => h.Id == fakeHomePageHighlightOne.Id));

        // Act
        var command = new DeleteHomePageHighlight.DeleteHomePageHighlightCommand(homePageHighlight.Id);
        await SendAsync(command);
        var deletedHomePageHighlight = await ExecuteDbContextAsync(db => db.HomePageHighlights
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == homePageHighlight.Id));

        // Assert
        deletedHomePageHighlight?.IsDeleted.Should().BeTrue();
    }
}