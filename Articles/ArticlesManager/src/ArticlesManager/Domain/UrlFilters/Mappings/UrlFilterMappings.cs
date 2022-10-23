namespace ArticlesManager.Domain.UrlFilters.Mappings;

using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters;
using Mapster;

public class UrlFilterMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UrlFilterDto, UrlFilter>()
            .TwoWays();
        config.NewConfig<UrlFilterForCreationDto, UrlFilter>()
            .TwoWays();
        config.NewConfig<UrlFilterForUpdateDto, UrlFilter>()
            .TwoWays();
    }
}