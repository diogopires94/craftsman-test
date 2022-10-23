namespace ArticlesManager.Domain.Collections.Validators;

using ArticlesManager.Domain.Collections.Dtos;
using FluentValidation;

public class CollectionForCreationDtoValidator: CollectionForManipulationDtoValidator<CollectionForCreationDto>
{
    public CollectionForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}