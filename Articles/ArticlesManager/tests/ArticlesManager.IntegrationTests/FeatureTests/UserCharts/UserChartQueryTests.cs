namespace ArticlesManager.IntegrationTests.FeatureTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.Domain.UserCharts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class UserChartQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_userchart_with_accurate_props()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeUserChartOne);

        // Act
        var query = new GetUserChart.UserChartQuery(fakeUserChartOne.Id);
        var userChart = await SendAsync(query);

        // Assert
        userChart.Should().BeEquivalentTo(fakeUserChartOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_userchart_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetUserChart.UserChartQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}