namespace ArticlesManager.Domain.Brands.Validators;

using ArticlesManager.Domain.Brands.Dtos;
using FluentValidation;

public class BrandForCreationDtoValidator: BrandForManipulationDtoValidator<BrandForCreationDto>
{
    public BrandForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}