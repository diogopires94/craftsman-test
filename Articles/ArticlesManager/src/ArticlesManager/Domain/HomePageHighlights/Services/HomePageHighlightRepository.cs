namespace ArticlesManager.Domain.HomePageHighlights.Services;

using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IHomePageHighlightRepository : IGenericRepository<HomePageHighlight>
{
}

public class HomePageHighlightRepository : GenericRepository<HomePageHighlight>, IHomePageHighlightRepository
{
    private readonly ArticlesDbContext _dbContext;

    public HomePageHighlightRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
