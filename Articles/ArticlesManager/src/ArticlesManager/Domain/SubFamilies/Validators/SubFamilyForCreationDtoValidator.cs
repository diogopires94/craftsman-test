namespace ArticlesManager.Domain.SubFamilies.Validators;

using ArticlesManager.Domain.SubFamilies.Dtos;
using FluentValidation;

public class SubFamilyForCreationDtoValidator: SubFamilyForManipulationDtoValidator<SubFamilyForCreationDto>
{
    public SubFamilyForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}