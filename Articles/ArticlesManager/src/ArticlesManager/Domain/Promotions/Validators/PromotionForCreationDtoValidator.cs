namespace ArticlesManager.Domain.Promotions.Validators;

using ArticlesManager.Domain.Promotions.Dtos;
using FluentValidation;

public class PromotionForCreationDtoValidator: PromotionForManipulationDtoValidator<PromotionForCreationDto>
{
    public PromotionForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}