namespace ArticlesManager.IntegrationTests.FeatureTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class PromotionQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_promotion_with_accurate_props()
    {
        // Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotionOne);

        // Act
        var query = new GetPromotion.PromotionQuery(fakePromotionOne.Id);
        var promotion = await SendAsync(query);

        // Assert
        promotion.Should().BeEquivalentTo(fakePromotionOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_promotion_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetPromotion.PromotionQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}