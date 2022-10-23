namespace ArticlesManager.FunctionalTests.FunctionalTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArticleTests : TestBase
{
    [Test]
    public async Task get_article_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeArticle = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticle);

        // Act
        var route = ApiRoutes.Articles.GetRecord.Replace(ApiRoutes.Articles.Id, fakeArticle.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}