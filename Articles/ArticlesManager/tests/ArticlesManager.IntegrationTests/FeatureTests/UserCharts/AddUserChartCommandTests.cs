namespace ArticlesManager.IntegrationTests.FeatureTests.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.UserCharts.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class AddUserChartCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_userchart_to_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeUserChartOne = new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate();

        // Act
        var command = new AddUserChart.AddUserChartCommand(fakeUserChartOne);
        var userChartReturned = await SendAsync(command);
        var userChartCreated = await ExecuteDbContextAsync(db => db.UserCharts
            .FirstOrDefaultAsync(u => u.Id == userChartReturned.Id));

        // Assert
        userChartReturned.Should().BeEquivalentTo(fakeUserChartOne, options =>
            options.ExcludingMissingMembers());
        userChartCreated.Should().BeEquivalentTo(fakeUserChartOne, options =>
            options.ExcludingMissingMembers());
    }
}