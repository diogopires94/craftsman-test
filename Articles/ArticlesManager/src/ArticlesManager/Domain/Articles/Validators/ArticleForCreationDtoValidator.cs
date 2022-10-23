namespace ArticlesManager.Domain.Articles.Validators;

using ArticlesManager.Domain.Articles.Dtos;
using FluentValidation;

public class ArticleForCreationDtoValidator: ArticleForManipulationDtoValidator<ArticleForCreationDto>
{
    public ArticleForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}