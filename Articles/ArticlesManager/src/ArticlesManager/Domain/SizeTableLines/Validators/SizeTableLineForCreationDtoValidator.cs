namespace ArticlesManager.Domain.SizeTableLines.Validators;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using FluentValidation;

public class SizeTableLineForCreationDtoValidator: SizeTableLineForManipulationDtoValidator<SizeTableLineForCreationDto>
{
    public SizeTableLineForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}