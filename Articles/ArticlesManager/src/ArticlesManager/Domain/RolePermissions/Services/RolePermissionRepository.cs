namespace ArticlesManager.Domain.RolePermissions.Services;

using ArticlesManager.Domain.RolePermissions;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
}

public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
{
    private readonly ArticlesDbContext _dbContext;

    public RolePermissionRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
