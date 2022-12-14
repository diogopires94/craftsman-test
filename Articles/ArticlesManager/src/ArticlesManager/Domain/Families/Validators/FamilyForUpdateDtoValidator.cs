namespace ArticlesManager.Domain.Families.Validators;

using ArticlesManager.Domain.Families.Dtos;
using FluentValidation;

public class FamilyForUpdateDtoValidator: FamilyForManipulationDtoValidator<FamilyForUpdateDto>
{
    public FamilyForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}