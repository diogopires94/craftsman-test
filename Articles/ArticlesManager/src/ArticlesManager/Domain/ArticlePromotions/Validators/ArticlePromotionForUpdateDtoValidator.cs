namespace ArticlesManager.Domain.ArticlePromotions.Validators;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using FluentValidation;

public class ArticlePromotionForUpdateDtoValidator: ArticlePromotionForManipulationDtoValidator<ArticlePromotionForUpdateDto>
{
    public ArticlePromotionForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}