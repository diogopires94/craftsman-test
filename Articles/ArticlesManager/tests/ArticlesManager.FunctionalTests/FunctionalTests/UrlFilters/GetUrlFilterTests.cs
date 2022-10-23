namespace ArticlesManager.FunctionalTests.FunctionalTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUrlFilterTests : TestBase
{
    [Test]
    public async Task get_urlfilter_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUrlFilter = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto().Generate());
        await InsertAsync(fakeUrlFilter);

        // Act
        var route = ApiRoutes.UrlFilters.GetRecord.Replace(ApiRoutes.UrlFilters.Id, fakeUrlFilter.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}