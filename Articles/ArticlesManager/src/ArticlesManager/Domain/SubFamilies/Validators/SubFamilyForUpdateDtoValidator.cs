namespace ArticlesManager.Domain.SubFamilies.Validators;

using ArticlesManager.Domain.SubFamilies.Dtos;
using FluentValidation;

public class SubFamilyForUpdateDtoValidator: SubFamilyForManipulationDtoValidator<SubFamilyForUpdateDto>
{
    public SubFamilyForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}