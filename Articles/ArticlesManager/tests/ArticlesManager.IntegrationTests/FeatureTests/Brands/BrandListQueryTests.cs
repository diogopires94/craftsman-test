namespace ArticlesManager.IntegrationTests.FeatureTests.Brands;

using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Brands.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class BrandListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_brand_list()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        var queryParameters = new BrandParametersDto();

        await InsertAsync(fakeBrandOne, fakeBrandTwo);

        // Act
        var query = new GetBrandList.BrandListQuery(queryParameters);
        var brands = await SendAsync(query);

        // Assert
        brands.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}