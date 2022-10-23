namespace ArticlesManager.FunctionalTests.FunctionalTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateBrandTests : TestBase
{
    [Test]
    public async Task create_brand_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeBrand = new FakeBrandForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Brands.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeBrand);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}