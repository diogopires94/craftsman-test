namespace ArticlesManager.Domain.ArticleImages.Mappings;

using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages;
using Mapster;

public class ArticleImageMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleImageDto, ArticleImage>()
            .TwoWays();
        config.NewConfig<ArticleImageForCreationDto, ArticleImage>()
            .TwoWays();
        config.NewConfig<ArticleImageForUpdateDto, ArticleImage>()
            .TwoWays();
    }
}