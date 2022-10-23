namespace ArticlesManager.FunctionalTests.FunctionalTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetPromotionListTests : TestBase
{
    [Test]
    public async Task get_promotion_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Promotions.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}