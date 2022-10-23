namespace ArticlesManager.Domain.HomePageHighlights.Mappings;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights;
using Mapster;

public class HomePageHighlightMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<HomePageHighlightDto, HomePageHighlight>()
            .TwoWays();
        config.NewConfig<HomePageHighlightForCreationDto, HomePageHighlight>()
            .TwoWays();
        config.NewConfig<HomePageHighlightForUpdateDto, HomePageHighlight>()
            .TwoWays();
    }
}