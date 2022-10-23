namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateArticleImageRecordTests : TestBase
{
    [Test]
    public async Task put_articleimage_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArticleImage = FakeArticleImage.Generate(new FakeArticleImageForCreationDto().Generate());
        var updatedArticleImageDto = new FakeArticleImageForUpdateDto { }.Generate();
        await InsertAsync(fakeArticleImage);

        // Act
        var route = ApiRoutes.ArticleImages.Put.Replace(ApiRoutes.ArticleImages.Id, fakeArticleImage.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedArticleImageDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}