namespace ArticlesManager.FunctionalTests.FunctionalTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUrlListTests : TestBase
{
    [Test]
    public async Task get_url_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Urls.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}