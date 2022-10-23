namespace ArticlesManager.IntegrationTests.FeatureTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Promotions.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddPromotionCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_promotion_to_db()
    {
        // Arrange
        var fakePromotionOne = new FakePromotionForCreationDto().Generate();

        // Act
        var command = new AddPromotion.AddPromotionCommand(fakePromotionOne);
        var promotionReturned = await SendAsync(command);
        var promotionCreated = await ExecuteDbContextAsync(db => db.Promotions
            .FirstOrDefaultAsync(p => p.Id == promotionReturned.Id));

        // Assert
        promotionReturned.Should().BeEquivalentTo(fakePromotionOne, options =>
            options.ExcludingMissingMembers());
        promotionCreated.Should().BeEquivalentTo(fakePromotionOne, options =>
            options.ExcludingMissingMembers());
    }
}