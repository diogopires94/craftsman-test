namespace ArticlesManager.Domain.UserCharts.Validators;

using ArticlesManager.Domain.UserCharts.Dtos;
using FluentValidation;

public class UserChartForCreationDtoValidator: UserChartForManipulationDtoValidator<UserChartForCreationDto>
{
    public UserChartForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}