namespace ArticlesManager.IntegrationTests.FeatureTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Promotions.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdatePromotionCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_promotion_in_db()
    {
        // Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        var updatedPromotionDto = new FakePromotionForUpdateDto().Generate();
        await InsertAsync(fakePromotionOne);

        var promotion = await ExecuteDbContextAsync(db => db.Promotions
            .FirstOrDefaultAsync(p => p.Id == fakePromotionOne.Id));
        var id = promotion.Id;

        // Act
        var command = new UpdatePromotion.UpdatePromotionCommand(id, updatedPromotionDto);
        await SendAsync(command);
        var updatedPromotion = await ExecuteDbContextAsync(db => db.Promotions.FirstOrDefaultAsync(p => p.Id == id));

        // Assert
        updatedPromotion.Should().BeEquivalentTo(updatedPromotionDto, options =>
            options.ExcludingMissingMembers());
    }
}