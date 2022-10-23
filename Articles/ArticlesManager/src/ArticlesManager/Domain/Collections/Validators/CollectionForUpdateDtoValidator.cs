namespace ArticlesManager.Domain.Collections.Validators;

using ArticlesManager.Domain.Collections.Dtos;
using FluentValidation;

public class CollectionForUpdateDtoValidator: CollectionForManipulationDtoValidator<CollectionForUpdateDto>
{
    public CollectionForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}