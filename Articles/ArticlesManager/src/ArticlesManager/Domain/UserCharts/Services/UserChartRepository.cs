namespace ArticlesManager.Domain.UserCharts.Services;

using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IUserChartRepository : IGenericRepository<UserChart>
{
}

public class UserChartRepository : GenericRepository<UserChart>, IUserChartRepository
{
    private readonly ArticlesDbContext _dbContext;

    public UserChartRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
