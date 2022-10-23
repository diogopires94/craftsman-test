namespace ArticlesManager.Domain.SizeTables.Validators;

using ArticlesManager.Domain.SizeTables.Dtos;
using FluentValidation;

public class SizeTableForUpdateDtoValidator: SizeTableForManipulationDtoValidator<SizeTableForUpdateDto>
{
    public SizeTableForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}