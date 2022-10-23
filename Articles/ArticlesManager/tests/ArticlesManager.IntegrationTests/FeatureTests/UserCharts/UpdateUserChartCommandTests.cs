namespace ArticlesManager.IntegrationTests.FeatureTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.Domain.UserCharts.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.UserCharts.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class UpdateUserChartCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_userchart_in_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var updatedUserChartDto = new FakeUserChartForUpdateDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate();
        await InsertAsync(fakeUserChartOne);

        var userChart = await ExecuteDbContextAsync(db => db.UserCharts
            .FirstOrDefaultAsync(u => u.Id == fakeUserChartOne.Id));
        var id = userChart.Id;

        // Act
        var command = new UpdateUserChart.UpdateUserChartCommand(id, updatedUserChartDto);
        await SendAsync(command);
        var updatedUserChart = await ExecuteDbContextAsync(db => db.UserCharts.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUserChart.Should().BeEquivalentTo(updatedUserChartDto, options =>
            options.ExcludingMissingMembers());
    }
}