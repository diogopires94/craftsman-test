namespace ArticlesManager.Domain.SizeTables.Validators;

using ArticlesManager.Domain.SizeTables.Dtos;
using FluentValidation;

public class SizeTableForCreationDtoValidator: SizeTableForManipulationDtoValidator<SizeTableForCreationDto>
{
    public SizeTableForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}