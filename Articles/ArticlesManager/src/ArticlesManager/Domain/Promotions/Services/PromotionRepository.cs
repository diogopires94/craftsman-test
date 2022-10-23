namespace ArticlesManager.Domain.Promotions.Services;

using ArticlesManager.Domain.Promotions;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IPromotionRepository : IGenericRepository<Promotion>
{
}

public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
{
    private readonly ArticlesDbContext _dbContext;

    public PromotionRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
