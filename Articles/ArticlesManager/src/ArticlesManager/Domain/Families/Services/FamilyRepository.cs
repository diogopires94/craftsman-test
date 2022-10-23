namespace ArticlesManager.Domain.Families.Services;

using ArticlesManager.Domain.Families;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IFamilyRepository : IGenericRepository<Family>
{
}

public class FamilyRepository : GenericRepository<Family>, IFamilyRepository
{
    private readonly ArticlesDbContext _dbContext;

    public FamilyRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
