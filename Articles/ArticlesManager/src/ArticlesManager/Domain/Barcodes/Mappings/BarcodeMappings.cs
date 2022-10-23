namespace ArticlesManager.Domain.Barcodes.Mappings;

using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes;
using Mapster;

public class BarcodeMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BarcodeDto, Barcode>()
            .TwoWays();
        config.NewConfig<BarcodeForCreationDto, Barcode>()
            .TwoWays();
        config.NewConfig<BarcodeForUpdateDto, Barcode>()
            .TwoWays();
    }
}