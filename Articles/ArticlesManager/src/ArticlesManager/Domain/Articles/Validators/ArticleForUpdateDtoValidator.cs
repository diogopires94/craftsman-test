namespace ArticlesManager.Domain.Articles.Validators;

using ArticlesManager.Domain.Articles.Dtos;
using FluentValidation;

public class ArticleForUpdateDtoValidator: ArticleForManipulationDtoValidator<ArticleForUpdateDto>
{
    public ArticleForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}