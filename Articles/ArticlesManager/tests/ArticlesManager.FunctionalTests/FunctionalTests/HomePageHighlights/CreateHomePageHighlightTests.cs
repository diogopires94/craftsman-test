namespace ArticlesManager.FunctionalTests.FunctionalTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateHomePageHighlightTests : TestBase
{
    [Test]
    public async Task create_homepagehighlight_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeHomePageHighlight = new FakeHomePageHighlightForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.HomePageHighlights.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeHomePageHighlight);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}