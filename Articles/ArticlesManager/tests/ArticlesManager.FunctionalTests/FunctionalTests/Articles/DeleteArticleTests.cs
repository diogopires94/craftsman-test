namespace ArticlesManager.FunctionalTests.FunctionalTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteArticleTests : TestBase
{
    [Test]
    public async Task delete_article_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArticle = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticle);

        // Act
        var route = ApiRoutes.Articles.Delete.Replace(ApiRoutes.Articles.Id, fakeArticle.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}