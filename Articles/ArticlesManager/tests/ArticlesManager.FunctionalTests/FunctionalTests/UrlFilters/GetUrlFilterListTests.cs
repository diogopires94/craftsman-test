namespace ArticlesManager.FunctionalTests.FunctionalTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUrlFilterListTests : TestBase
{
    [Test]
    public async Task get_urlfilter_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.UrlFilters.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}