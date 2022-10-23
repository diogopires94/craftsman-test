namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateArticleImageTests : TestBase
{
    [Test]
    public async Task create_articleimage_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeArticleImage = new FakeArticleImageForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.ArticleImages.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeArticleImage);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}