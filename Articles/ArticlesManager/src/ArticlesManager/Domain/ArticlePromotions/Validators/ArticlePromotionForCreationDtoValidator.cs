namespace ArticlesManager.Domain.ArticlePromotions.Validators;

using ArticlesManager.Domain.ArticlePromotions.Dtos;
using FluentValidation;

public class ArticlePromotionForCreationDtoValidator: ArticlePromotionForManipulationDtoValidator<ArticlePromotionForCreationDto>
{
    public ArticlePromotionForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}