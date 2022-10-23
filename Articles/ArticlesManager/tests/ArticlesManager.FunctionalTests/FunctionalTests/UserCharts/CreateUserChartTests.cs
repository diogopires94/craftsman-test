namespace ArticlesManager.FunctionalTests.FunctionalTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateUserChartTests : TestBase
{
    [Test]
    public async Task create_userchart_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUserChart = new FakeUserChartForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.UserCharts.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeUserChart);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}