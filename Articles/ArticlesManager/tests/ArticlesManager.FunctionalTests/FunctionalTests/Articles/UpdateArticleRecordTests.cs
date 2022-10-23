namespace ArticlesManager.FunctionalTests.FunctionalTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateArticleRecordTests : TestBase
{
    [Test]
    public async Task put_article_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArticle = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        var updatedArticleDto = new FakeArticleForUpdateDto { }.Generate();
        await InsertAsync(fakeArticle);

        // Act
        var route = ApiRoutes.Articles.Put.Replace(ApiRoutes.Articles.Id, fakeArticle.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedArticleDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}