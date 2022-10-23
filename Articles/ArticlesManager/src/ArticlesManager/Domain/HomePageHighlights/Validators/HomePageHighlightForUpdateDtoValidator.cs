namespace ArticlesManager.Domain.HomePageHighlights.Validators;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using FluentValidation;

public class HomePageHighlightForUpdateDtoValidator: HomePageHighlightForManipulationDtoValidator<HomePageHighlightForUpdateDto>
{
    public HomePageHighlightForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}