namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArticleImageListTests : TestBase
{
    [Test]
    public async Task get_articleimage_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.ArticleImages.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}