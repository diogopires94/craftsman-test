namespace ArticlesManager.SharedTestHelpers.Fakes.Url;

using AutoBogus;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.Dtos;

public class FakeUrl
{
    public static Url Generate(UrlForCreationDto urlForCreationDto)
    {
        return Url.Create(urlForCreationDto);
    }

    public static Url Generate()
    {
        return Url.Create(new FakeUrlForCreationDto().Generate());
    }
}