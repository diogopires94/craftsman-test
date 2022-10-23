namespace ArticlesManager.Domain.Urls.Mappings;

using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls;
using Mapster;

public class UrlMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UrlDto, Url>()
            .TwoWays();
        config.NewConfig<UrlForCreationDto, Url>()
            .TwoWays();
        config.NewConfig<UrlForUpdateDto, Url>()
            .TwoWays();
    }
}