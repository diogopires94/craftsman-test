namespace ArticlesManager.IntegrationTests.FeatureTests.HomePageHighlights;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.HomePageHighlights.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class HomePageHighlightListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_homepagehighlight_list()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    await InsertAsync(fakeArticleOne, fakeArticleTwo);

        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    await InsertAsync(fakeBrandOne, fakeBrandTwo);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    await InsertAsync(fakeCollectionOne, fakeCollectionTwo);

        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.ArticleId, _ => fakeArticleOne.Id)
            
            .RuleFor(h => h.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(h => h.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.ArticleId, _ => fakeArticleTwo.Id)
            
            .RuleFor(h => h.BrandId, _ => fakeBrandTwo.Id)
            
            .RuleFor(h => h.CollectionId, _ => fakeCollectionTwo.Id)
            .Generate());
        var queryParameters = new HomePageHighlightParametersDto();

        await InsertAsync(fakeHomePageHighlightOne, fakeHomePageHighlightTwo);

        // Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var homePageHighlights = await SendAsync(query);

        // Assert
        homePageHighlights.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}