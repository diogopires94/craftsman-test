namespace ArticlesManager.FunctionalTests.FunctionalTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteUserChartTests : TestBase
{
    [Test]
    public async Task delete_userchart_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserChart = FakeUserChart.Generate(new FakeUserChartForCreationDto().Generate());
        await InsertAsync(fakeUserChart);

        // Act
        var route = ApiRoutes.UserCharts.Delete.Replace(ApiRoutes.UserCharts.Id, fakeUserChart.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}