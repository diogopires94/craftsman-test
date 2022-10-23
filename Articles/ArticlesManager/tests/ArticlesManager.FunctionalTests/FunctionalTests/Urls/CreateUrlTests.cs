namespace ArticlesManager.FunctionalTests.FunctionalTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateUrlTests : TestBase
{
    [Test]
    public async Task create_url_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUrl = new FakeUrlForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Urls.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeUrl);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}