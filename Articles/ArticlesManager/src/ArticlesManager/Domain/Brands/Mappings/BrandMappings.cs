namespace ArticlesManager.Domain.Brands.Mappings;

using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands;
using Mapster;

public class BrandMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BrandDto, Brand>()
            .TwoWays();
        config.NewConfig<BrandForCreationDto, Brand>()
            .TwoWays();
        config.NewConfig<BrandForUpdateDto, Brand>()
            .TwoWays();
    }
}