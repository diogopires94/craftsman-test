namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArticlePromotionTests : TestBase
{
    [Test]
    public async Task get_articlepromotion_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeArticlePromotion = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto().Generate());
        await InsertAsync(fakeArticlePromotion);

        // Act
        var route = ApiRoutes.ArticlePromotions.GetRecord.Replace(ApiRoutes.ArticlePromotions.Id, fakeArticlePromotion.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}