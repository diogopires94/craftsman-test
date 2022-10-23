namespace ArticlesManager.Domain.UserCharts.Validators;

using ArticlesManager.Domain.UserCharts.Dtos;
using FluentValidation;

public class UserChartForUpdateDtoValidator: UserChartForManipulationDtoValidator<UserChartForUpdateDto>
{
    public UserChartForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}