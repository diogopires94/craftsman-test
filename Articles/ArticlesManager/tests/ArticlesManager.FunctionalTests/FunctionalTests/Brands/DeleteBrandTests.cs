namespace ArticlesManager.FunctionalTests.FunctionalTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteBrandTests : TestBase
{
    [Test]
    public async Task delete_brand_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBrand = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrand);

        // Act
        var route = ApiRoutes.Brands.Delete.Replace(ApiRoutes.Brands.Id, fakeBrand.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}