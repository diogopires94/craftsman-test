namespace ArticlesManager.FunctionalTests.FunctionalTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetHomePageHighlightListTests : TestBase
{
    [Test]
    public async Task get_homepagehighlight_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.HomePageHighlights.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}