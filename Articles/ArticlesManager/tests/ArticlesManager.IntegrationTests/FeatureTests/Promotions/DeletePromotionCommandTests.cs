namespace ArticlesManager.IntegrationTests.FeatureTests.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeletePromotionCommandTests : TestBase
{
    [Test]
    public async Task can_delete_promotion_from_db()
    {
        // Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotionOne);
        var promotion = await ExecuteDbContextAsync(db => db.Promotions
            .FirstOrDefaultAsync(p => p.Id == fakePromotionOne.Id));

        // Act
        var command = new DeletePromotion.DeletePromotionCommand(promotion.Id);
        await SendAsync(command);
        var promotionResponse = await ExecuteDbContextAsync(db => db.Promotions.CountAsync(p => p.Id == promotion.Id));

        // Assert
        promotionResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_promotion_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeletePromotion.DeletePromotionCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_promotion_from_db()
    {
        // Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto().Generate());
        await InsertAsync(fakePromotionOne);
        var promotion = await ExecuteDbContextAsync(db => db.Promotions
            .FirstOrDefaultAsync(p => p.Id == fakePromotionOne.Id));

        // Act
        var command = new DeletePromotion.DeletePromotionCommand(promotion.Id);
        await SendAsync(command);
        var deletedPromotion = await ExecuteDbContextAsync(db => db.Promotions
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == promotion.Id));

        // Assert
        deletedPromotion?.IsDeleted.Should().BeTrue();
    }
}