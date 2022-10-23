namespace ArticlesManager.FunctionalTests.FunctionalTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateUrlFilterTests : TestBase
{
    [Test]
    public async Task create_urlfilter_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUrlFilter = new FakeUrlFilterForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.UrlFilters.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeUrlFilter);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}