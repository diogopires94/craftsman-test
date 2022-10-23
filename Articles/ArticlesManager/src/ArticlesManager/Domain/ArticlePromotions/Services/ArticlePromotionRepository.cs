namespace ArticlesManager.Domain.ArticlePromotions.Services;

using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IArticlePromotionRepository : IGenericRepository<ArticlePromotion>
{
}

public class ArticlePromotionRepository : GenericRepository<ArticlePromotion>, IArticlePromotionRepository
{
    private readonly ArticlesDbContext _dbContext;

    public ArticlePromotionRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
