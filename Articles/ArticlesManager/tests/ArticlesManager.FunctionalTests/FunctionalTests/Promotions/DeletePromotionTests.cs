namespace ArticlesManager.FunctionalTests.FunctionalTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeletePromotionTests : TestBase
{
    [Test]
    public async Task delete_promotion_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePromotion = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotion);

        // Act
        var route = ApiRoutes.Promotions.Delete.Replace(ApiRoutes.Promotions.Id, fakePromotion.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}