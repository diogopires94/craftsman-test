namespace ArticlesManager.IntegrationTests.FeatureTests.Promotions;

using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Promotions.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class PromotionListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_promotion_list()
    {
        // Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        var queryParameters = new PromotionParametersDto();

        await InsertAsync(fakePromotionOne, fakePromotionTwo);

        // Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var promotions = await SendAsync(query);

        // Assert
        promotions.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}