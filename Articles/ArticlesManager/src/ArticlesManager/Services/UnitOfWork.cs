namespace ArticlesManager.Services;

using ArticlesManager.Databases;

public interface IUnitOfWork : IArticlesManagerService
{
    Task CommitChanges(CancellationToken cancellationToken = default);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ArticlesDbContext _dbContext;

    public UnitOfWork(ArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
