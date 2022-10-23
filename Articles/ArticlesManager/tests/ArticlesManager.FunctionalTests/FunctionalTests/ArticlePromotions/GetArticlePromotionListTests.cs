namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArticlePromotionListTests : TestBase
{
    [Test]
    public async Task get_articlepromotion_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.ArticlePromotions.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}