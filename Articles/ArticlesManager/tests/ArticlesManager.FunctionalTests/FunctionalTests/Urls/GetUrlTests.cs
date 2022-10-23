namespace ArticlesManager.FunctionalTests.FunctionalTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUrlTests : TestBase
{
    [Test]
    public async Task get_url_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUrl = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        await InsertAsync(fakeUrl);

        // Act
        var route = ApiRoutes.Urls.GetRecord.Replace(ApiRoutes.Urls.Id, fakeUrl.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}