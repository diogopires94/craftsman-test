namespace ArticlesManager.Domain.SizeTables.Services;

using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface ISizeTableRepository : IGenericRepository<SizeTable>
{
}

public class SizeTableRepository : GenericRepository<SizeTable>, ISizeTableRepository
{
    private readonly ArticlesDbContext _dbContext;

    public SizeTableRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
