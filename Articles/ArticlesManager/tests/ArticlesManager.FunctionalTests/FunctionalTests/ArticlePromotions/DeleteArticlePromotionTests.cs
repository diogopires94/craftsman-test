namespace ArticlesManager.FunctionalTests.FunctionalTests.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteArticlePromotionTests : TestBase
{
    [Test]
    public async Task delete_articlepromotion_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArticlePromotion = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto().Generate());
        await InsertAsync(fakeArticlePromotion);

        // Act
        var route = ApiRoutes.ArticlePromotions.Delete.Replace(ApiRoutes.ArticlePromotions.Id, fakeArticlePromotion.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}