namespace ArticlesManager.Domain.Urls.Validators;

using ArticlesManager.Domain.Urls.Dtos;
using FluentValidation;

public class UrlForUpdateDtoValidator: UrlForManipulationDtoValidator<UrlForUpdateDto>
{
    public UrlForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}