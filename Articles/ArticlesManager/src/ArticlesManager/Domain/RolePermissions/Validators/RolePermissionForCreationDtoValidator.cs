namespace ArticlesManager.Domain.RolePermissions.Validators;

using ArticlesManager.Domain.RolePermissions.Dtos;
using FluentValidation;

public class RolePermissionForCreationDtoValidator: RolePermissionForManipulationDtoValidator<RolePermissionForCreationDto>
{
    public RolePermissionForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}