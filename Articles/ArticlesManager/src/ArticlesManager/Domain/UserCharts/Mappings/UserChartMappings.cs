namespace ArticlesManager.Domain.UserCharts.Mappings;

using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts;
using Mapster;

public class UserChartMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserChartDto, UserChart>()
            .TwoWays();
        config.NewConfig<UserChartForCreationDto, UserChart>()
            .TwoWays();
        config.NewConfig<UserChartForUpdateDto, UserChart>()
            .TwoWays();
    }
}