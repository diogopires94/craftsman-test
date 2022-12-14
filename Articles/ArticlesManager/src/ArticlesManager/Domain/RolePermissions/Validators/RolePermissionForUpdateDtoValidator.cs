namespace ArticlesManager.Domain.RolePermissions.Validators;

using ArticlesManager.Domain.RolePermissions.Dtos;
using FluentValidation;

public class RolePermissionForUpdateDtoValidator: RolePermissionForManipulationDtoValidator<RolePermissionForUpdateDto>
{
    public RolePermissionForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}