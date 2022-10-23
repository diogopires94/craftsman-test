namespace ArticlesManager.FunctionalTests.FunctionalTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUserChartListTests : TestBase
{
    [Test]
    public async Task get_userchart_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.UserCharts.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}