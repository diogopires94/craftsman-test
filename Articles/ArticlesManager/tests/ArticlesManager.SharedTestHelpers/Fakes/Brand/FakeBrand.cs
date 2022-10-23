namespace ArticlesManager.SharedTestHelpers.Fakes.Brand;

using AutoBogus;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.Dtos;

public class FakeBrand
{
    public static Brand Generate(BrandForCreationDto brandForCreationDto)
    {
        return Brand.Create(brandForCreationDto);
    }

    public static Brand Generate()
    {
        return Brand.Create(new FakeBrandForCreationDto().Generate());
    }
}