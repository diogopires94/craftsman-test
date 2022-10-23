namespace ArticlesManager.FunctionalTests.FunctionalTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateArticleTests : TestBase
{
    [Test]
    public async Task create_article_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeArticle = new FakeArticleForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Articles.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeArticle);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}