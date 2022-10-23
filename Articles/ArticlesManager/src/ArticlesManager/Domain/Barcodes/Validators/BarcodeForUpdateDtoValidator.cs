namespace ArticlesManager.Domain.Barcodes.Validators;

using ArticlesManager.Domain.Barcodes.Dtos;
using FluentValidation;

public class BarcodeForUpdateDtoValidator: BarcodeForManipulationDtoValidator<BarcodeForUpdateDto>
{
    public BarcodeForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}