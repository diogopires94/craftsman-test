namespace ArticlesManager.Domain.HomePageHighlights.Validators;

using ArticlesManager.Domain.HomePageHighlights.Dtos;
using FluentValidation;

public class HomePageHighlightForCreationDtoValidator: HomePageHighlightForManipulationDtoValidator<HomePageHighlightForCreationDto>
{
    public HomePageHighlightForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}