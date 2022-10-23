namespace ArticlesManager.Domain.SizeTableLines.Validators;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using FluentValidation;

public class SizeTableLineForUpdateDtoValidator: SizeTableLineForManipulationDtoValidator<SizeTableLineForUpdateDto>
{
    public SizeTableLineForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}