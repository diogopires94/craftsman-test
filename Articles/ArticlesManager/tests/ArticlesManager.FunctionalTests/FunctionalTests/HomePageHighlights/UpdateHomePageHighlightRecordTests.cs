namespace ArticlesManager.FunctionalTests.FunctionalTests.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateHomePageHighlightRecordTests : TestBase
{
    [Test]
    public async Task put_homepagehighlight_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto().Generate());
        var updatedHomePageHighlightDto = new FakeHomePageHighlightForUpdateDto { }.Generate();
        await InsertAsync(fakeHomePageHighlight);

        // Act
        var route = ApiRoutes.HomePageHighlights.Put.Replace(ApiRoutes.HomePageHighlights.Id, fakeHomePageHighlight.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedHomePageHighlightDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}