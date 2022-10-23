namespace ArticlesManager.FunctionalTests.FunctionalTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdatePromotionRecordTests : TestBase
{
    [Test]
    public async Task put_promotion_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePromotion = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        var updatedPromotionDto = new FakePromotionForUpdateDto { }.Generate();
        await InsertAsync(fakePromotion);

        // Act
        var route = ApiRoutes.Promotions.Put.Replace(ApiRoutes.Promotions.Id, fakePromotion.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedPromotionDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}