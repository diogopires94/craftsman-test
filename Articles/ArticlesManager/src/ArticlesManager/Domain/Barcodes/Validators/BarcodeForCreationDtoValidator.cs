namespace ArticlesManager.Domain.Barcodes.Validators;

using ArticlesManager.Domain.Barcodes.Dtos;
using FluentValidation;

public class BarcodeForCreationDtoValidator: BarcodeForManipulationDtoValidator<BarcodeForCreationDto>
{
    public BarcodeForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}