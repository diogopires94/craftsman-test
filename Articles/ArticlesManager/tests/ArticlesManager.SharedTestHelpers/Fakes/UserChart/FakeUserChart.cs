namespace ArticlesManager.SharedTestHelpers.Fakes.UserChart;

using AutoBogus;
using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.Dtos;

public class FakeUserChart
{
    public static UserChart Generate(UserChartForCreationDto userChartForCreationDto)
    {
        return UserChart.Create(userChartForCreationDto);
    }

    public static UserChart Generate()
    {
        return UserChart.Create(new FakeUserChartForCreationDto().Generate());
    }
}