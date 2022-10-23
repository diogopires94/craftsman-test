namespace ArticlesManager.FunctionalTests.FunctionalTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBrandTests : TestBase
{
    [Test]
    public async Task get_brand_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeBrand = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrand);

        // Act
        var route = ApiRoutes.Brands.GetRecord.Replace(ApiRoutes.Brands.Id, fakeBrand.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}