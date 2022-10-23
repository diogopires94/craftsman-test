namespace ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;

using AutoBogus;
using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.Dtos;

public class FakeArticleImage
{
    public static ArticleImage Generate(ArticleImageForCreationDto articleImageForCreationDto)
    {
        return ArticleImage.Create(articleImageForCreationDto);
    }

    public static ArticleImage Generate()
    {
        return ArticleImage.Create(new FakeArticleImageForCreationDto().Generate());
    }
}