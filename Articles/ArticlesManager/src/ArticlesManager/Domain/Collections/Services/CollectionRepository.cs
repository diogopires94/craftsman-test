namespace ArticlesManager.Domain.Collections.Services;

using ArticlesManager.Domain.Collections;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface ICollectionRepository : IGenericRepository<Collection>
{
}

public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
{
    private readonly ArticlesDbContext _dbContext;

    public CollectionRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
