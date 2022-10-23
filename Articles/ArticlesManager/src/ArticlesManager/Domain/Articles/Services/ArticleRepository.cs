namespace ArticlesManager.Domain.Articles.Services;

using ArticlesManager.Domain.Articles;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IArticleRepository : IGenericRepository<Article>
{
}

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    private readonly ArticlesDbContext _dbContext;

    public ArticleRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
