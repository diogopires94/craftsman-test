namespace ArticlesManager.Domain.UrlFilters.Validators;

using ArticlesManager.Domain.UrlFilters.Dtos;
using FluentValidation;

public class UrlFilterForUpdateDtoValidator: UrlFilterForManipulationDtoValidator<UrlFilterForUpdateDto>
{
    public UrlFilterForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}