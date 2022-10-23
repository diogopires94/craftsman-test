namespace ArticlesManager.FunctionalTests.FunctionalTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetHomePageHighlightTests : TestBase
{
    [Test]
    public async Task get_homepagehighlight_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto().Generate());
        await InsertAsync(fakeHomePageHighlight);

        // Act
        var route = ApiRoutes.HomePageHighlights.GetRecord.Replace(ApiRoutes.HomePageHighlights.Id, fakeHomePageHighlight.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}