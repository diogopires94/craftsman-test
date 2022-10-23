namespace ArticlesManager.FunctionalTests.FunctionalTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateBrandRecordTests : TestBase
{
    [Test]
    public async Task put_brand_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBrand = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        var updatedBrandDto = new FakeBrandForUpdateDto { }.Generate();
        await InsertAsync(fakeBrand);

        // Act
        var route = ApiRoutes.Brands.Put.Replace(ApiRoutes.Brands.Id, fakeBrand.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedBrandDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}