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

public class DeleteUserChartCommandTests : TestBase
{
    [Test]
    public async Task can_delete_userchart_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeUserChartOne);
        var userChart = await ExecuteDbContextAsync(db => db.UserCharts
            .FirstOrDefaultAsync(u => u.Id == fakeUserChartOne.Id));

        // Act
        var command = new DeleteUserChart.DeleteUserChartCommand(userChart.Id);
        await SendAsync(command);
        var userChartResponse = await ExecuteDbContextAsync(db => db.UserCharts.CountAsync(u => u.Id == userChart.Id));

        // Assert
        userChartResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_userchart_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteUserChart.DeleteUserChartCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_userchart_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeUserChartOne);
        var userChart = await ExecuteDbContextAsync(db => db.UserCharts
            .FirstOrDefaultAsync(u => u.Id == fakeUserChartOne.Id));

        // Act
        var command = new DeleteUserChart.DeleteUserChartCommand(userChart.Id);
        await SendAsync(command);
        var deletedUserChart = await ExecuteDbContextAsync(db => db.UserCharts
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == userChart.Id));

        // Assert
        deletedUserChart?.IsDeleted.Should().BeTrue();
    }
}