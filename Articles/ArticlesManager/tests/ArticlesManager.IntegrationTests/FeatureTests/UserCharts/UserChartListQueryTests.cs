namespace ArticlesManager.IntegrationTests.FeatureTests.UserCharts;

using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.UserCharts.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class UserChartListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_userchart_list()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    await InsertAsync(fakeArticleOne, fakeArticleTwo);

        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var fakeUserChartTwo = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.ArticleId, _ => fakeArticleTwo.Id)
            .Generate());
        var queryParameters = new UserChartParametersDto();

        await InsertAsync(fakeUserChartOne, fakeUserChartTwo);

        // Act
        var query = new GetUserChartList.UserChartListQuery(queryParameters);
        var userCharts = await SendAsync(query);

        // Assert
        userCharts.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}