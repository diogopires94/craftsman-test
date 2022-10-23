namespace ArticlesManager.FunctionalTests.FunctionalTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetPromotionTests : TestBase
{
    [Test]
    public async Task get_promotion_returns_success_when_entity_exists()
    {
        // Arrange
        var fakePromotion = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotion);

        // Act
        var route = ApiRoutes.Promotions.GetRecord.Replace(ApiRoutes.Promotions.Id, fakePromotion.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}