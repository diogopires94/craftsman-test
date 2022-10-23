namespace ArticlesManager.Domain.ArticleImages.Validators;

using ArticlesManager.Domain.ArticleImages.Dtos;
using FluentValidation;

public class ArticleImageForUpdateDtoValidator: ArticleImageForManipulationDtoValidator<ArticleImageForUpdateDto>
{
    public ArticleImageForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}