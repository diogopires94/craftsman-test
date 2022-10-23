namespace ArticlesManager.Domain.Urls.Services;

using ArticlesManager.Domain.Urls;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IUrlRepository : IGenericRepository<Url>
{
}

public class UrlRepository : GenericRepository<Url>, IUrlRepository
{
    private readonly ArticlesDbContext _dbContext;

    public UrlRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
