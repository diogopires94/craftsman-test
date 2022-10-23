namespace ArticlesManager.Domain.Promotions.Validators;

using ArticlesManager.Domain.Promotions.Dtos;
using FluentValidation;

public class PromotionForUpdateDtoValidator: PromotionForManipulationDtoValidator<PromotionForUpdateDto>
{
    public PromotionForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}