namespace ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;

using AutoBogus;
using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.Dtos;

public class FakeHomePageHighlight
{
    public static HomePageHighlight Generate(HomePageHighlightForCreationDto homePageHighlightForCreationDto)
    {
        return HomePageHighlight.Create(homePageHighlightForCreationDto);
    }

    public static HomePageHighlight Generate()
    {
        return HomePageHighlight.Create(new FakeHomePageHighlightForCreationDto().Generate());
    }
}