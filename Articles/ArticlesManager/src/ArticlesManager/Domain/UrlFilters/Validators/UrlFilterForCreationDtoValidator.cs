namespace ArticlesManager.Domain.UrlFilters.Validators;

using ArticlesManager.Domain.UrlFilters.Dtos;
using FluentValidation;

public class UrlFilterForCreationDtoValidator: UrlFilterForManipulationDtoValidator<UrlFilterForCreationDto>
{
    public UrlFilterForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}