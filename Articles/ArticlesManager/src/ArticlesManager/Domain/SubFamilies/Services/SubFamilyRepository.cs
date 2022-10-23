namespace ArticlesManager.Domain.SubFamilies.Services;

using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface ISubFamilyRepository : IGenericRepository<SubFamily>
{
}

public class SubFamilyRepository : GenericRepository<SubFamily>, ISubFamilyRepository
{
    private readonly ArticlesDbContext _dbContext;

    public SubFamilyRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
