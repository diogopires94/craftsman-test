namespace ArticlesManager.Domain.Families.Validators;

using ArticlesManager.Domain.Families.Dtos;
using FluentValidation;

public class FamilyForCreationDtoValidator: FamilyForManipulationDtoValidator<FamilyForCreationDto>
{
    public FamilyForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}