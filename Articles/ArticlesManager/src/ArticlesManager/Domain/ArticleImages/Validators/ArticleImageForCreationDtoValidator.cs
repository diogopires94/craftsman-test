namespace ArticlesManager.Domain.ArticleImages.Validators;

using ArticlesManager.Domain.ArticleImages.Dtos;
using FluentValidation;

public class ArticleImageForCreationDtoValidator: ArticleImageForManipulationDtoValidator<ArticleImageForCreationDto>
{
    public ArticleImageForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}