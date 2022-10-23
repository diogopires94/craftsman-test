namespace ArticlesManager.Domain.ArticleImages.Services;

using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IArticleImageRepository : IGenericRepository<ArticleImage>
{
}

public class ArticleImageRepository : GenericRepository<ArticleImage>, IArticleImageRepository
{
    private readonly ArticlesDbContext _dbContext;

    public ArticleImageRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
