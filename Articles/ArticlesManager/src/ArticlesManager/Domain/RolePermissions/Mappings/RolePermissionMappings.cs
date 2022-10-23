namespace ArticlesManager.Domain.RolePermissions.Mappings;

using ArticlesManager.Domain.RolePermissions.Dtos;
using ArticlesManager.Domain.RolePermissions;
using Mapster;

public class RolePermissionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RolePermissionDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForCreationDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForUpdateDto, RolePermission>()
            .TwoWays();
    }
}