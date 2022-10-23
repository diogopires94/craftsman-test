namespace ArticlesManager.Domain.SizeTableLines.Services;

using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface ISizeTableLineRepository : IGenericRepository<SizeTableLine>
{
}

public class SizeTableLineRepository : GenericRepository<SizeTableLine>, ISizeTableLineRepository
{
    private readonly ArticlesDbContext _dbContext;

    public SizeTableLineRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
