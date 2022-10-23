namespace ArticlesManager.Domain.Brands.Validators;

using ArticlesManager.Domain.Brands.Dtos;
using FluentValidation;

public class BrandForUpdateDtoValidator: BrandForManipulationDtoValidator<BrandForUpdateDto>
{
    public BrandForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}