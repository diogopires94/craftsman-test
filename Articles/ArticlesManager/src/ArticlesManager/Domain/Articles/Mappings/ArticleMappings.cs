namespace ArticlesManager.Domain.Articles.Mappings;

using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles;
using Mapster;

public class ArticleMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleDto, Article>()
            .TwoWays();
        config.NewConfig<ArticleForCreationDto, Article>()
            .TwoWays();
        config.NewConfig<ArticleForUpdateDto, Article>()
            .TwoWays();
    }
}