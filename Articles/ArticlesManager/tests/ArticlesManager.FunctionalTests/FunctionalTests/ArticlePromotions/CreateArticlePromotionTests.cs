namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateArticlePromotionTests : TestBase
{
    [Test]
    public async Task create_articlepromotion_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeArticlePromotion = new FakeArticlePromotionForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.ArticlePromotions.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeArticlePromotion);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}