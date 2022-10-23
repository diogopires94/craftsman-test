namespace ArticlesManager.FunctionalTests.FunctionalTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteHomePageHighlightTests : TestBase
{
    [Test]
    public async Task delete_homepagehighlight_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto().Generate());
        await InsertAsync(fakeHomePageHighlight);

        // Act
        var route = ApiRoutes.HomePageHighlights.Delete.Replace(ApiRoutes.HomePageHighlights.Id, fakeHomePageHighlight.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}