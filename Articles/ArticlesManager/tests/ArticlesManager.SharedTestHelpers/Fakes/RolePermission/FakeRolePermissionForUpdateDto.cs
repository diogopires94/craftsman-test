namespace ArticlesManager.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using ArticlesManager.Domain;
using ArticlesManager.Domain.RolePermissions.Dtos;
using SharedKernel.Domain;

public class FakeRolePermissionForUpdateDto : AutoFaker<RolePermissionForUpdateDto>
{
    public FakeRolePermissionForUpdateDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Roles.List()));
    }
}