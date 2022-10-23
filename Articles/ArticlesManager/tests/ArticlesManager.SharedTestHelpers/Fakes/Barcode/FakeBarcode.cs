namespace ArticlesManager.SharedTestHelpers.Fakes.Barcode;

using AutoBogus;
using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.Dtos;

public class FakeBarcode
{
    public static Barcode Generate(BarcodeForCreationDto barcodeForCreationDto)
    {
        return Barcode.Create(barcodeForCreationDto);
    }

    public static Barcode Generate()
    {
        return Barcode.Create(new FakeBarcodeForCreationDto().Generate());
    }
}