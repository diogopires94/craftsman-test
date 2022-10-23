namespace ArticlesManager.Domain.Brands.Services;

using ArticlesManager.Domain.Brands;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IBrandRepository : IGenericRepository<Brand>
{
}

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
    private readonly ArticlesDbContext _dbContext;

    public BrandRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
