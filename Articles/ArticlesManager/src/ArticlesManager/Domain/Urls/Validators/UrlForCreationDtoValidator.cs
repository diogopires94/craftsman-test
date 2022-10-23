namespace ArticlesManager.Domain.Urls.Validators;

using ArticlesManager.Domain.Urls.Dtos;
using FluentValidation;

public class UrlForCreationDtoValidator: UrlForManipulationDtoValidator<UrlForCreationDto>
{
    public UrlForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}