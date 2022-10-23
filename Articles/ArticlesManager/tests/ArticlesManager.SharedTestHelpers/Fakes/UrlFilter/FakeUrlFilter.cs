namespace ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;

using AutoBogus;
using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.Dtos;

public class FakeUrlFilter
{
    public static UrlFilter Generate(UrlFilterForCreationDto urlFilterForCreationDto)
    {
        return UrlFilter.Create(urlFilterForCreationDto);
    }

    public static UrlFilter Generate()
    {
        return UrlFilter.Create(new FakeUrlFilterForCreationDto().Generate());
    }
}