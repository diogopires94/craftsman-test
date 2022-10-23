namespace ArticlesManager.SharedTestHelpers.Fakes.Article;

using AutoBogus;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.Dtos;

public class FakeArticle
{
    public static Article Generate(ArticleForCreationDto articleForCreationDto)
    {
        return Article.Create(articleForCreationDto);
    }

    public static Article Generate()
    {
        return Article.Create(new FakeArticleForCreationDto().Generate());
    }
}