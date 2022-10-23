namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteArticleImageTests : TestBase
{
    [Test]
    public async Task delete_articleimage_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArticleImage = FakeArticleImage.Generate(new FakeArticleImageForCreationDto().Generate());
        await InsertAsync(fakeArticleImage);

        // Act
        var route = ApiRoutes.ArticleImages.Delete.Replace(ApiRoutes.ArticleImages.Id, fakeArticleImage.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}