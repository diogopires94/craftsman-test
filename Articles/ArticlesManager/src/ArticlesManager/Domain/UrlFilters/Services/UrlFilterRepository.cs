namespace ArticlesManager.Domain.UrlFilters.Services;

using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IUrlFilterRepository : IGenericRepository<UrlFilter>
{
}

public class UrlFilterRepository : GenericRepository<UrlFilter>, IUrlFilterRepository
{
    private readonly ArticlesDbContext _dbContext;

    public UrlFilterRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
