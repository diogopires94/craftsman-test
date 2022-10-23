namespace ArticlesManager.FunctionalTests.FunctionalTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBrandListTests : TestBase
{
    [Test]
    public async Task get_brand_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Brands.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}