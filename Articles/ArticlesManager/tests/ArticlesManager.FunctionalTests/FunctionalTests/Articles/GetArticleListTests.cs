namespace ArticlesManager.FunctionalTests.FunctionalTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArticleListTests : TestBase
{
    [Test]
    public async Task get_article_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Articles.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}