namespace ArticlesManager.FunctionalTests.FunctionalTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreatePromotionTests : TestBase
{
    [Test]
    public async Task create_promotion_returns_created_using_valid_dto()
    {
        // Arrange
        var fakePromotion = new FakePromotionForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Promotions.Create;
        var result = await _client.PostJsonRequestAsync(route, fakePromotion);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}