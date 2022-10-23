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

public class HomePageHighlightQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_homepagehighlight_with_accurate_props()
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

        // Act
        var query = new GetHomePageHighlight.HomePageHighlightQuery(fakeHomePageHighlightOne.Id);
        var homePageHighlight = await SendAsync(query);

        // Assert
        homePageHighlight.Should().BeEquivalentTo(fakeHomePageHighlightOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_homepagehighlight_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetHomePageHighlight.HomePageHighlightQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}