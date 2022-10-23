namespace ArticlesManager.UnitTests.UnitTests.Domain.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdatePromotionTests
{
    private readonly Faker _faker;

    public UpdatePromotionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_promotion()
    {
        // Arrange
        var fakePromotion = FakePromotion.Generate();
        var updatedPromotion = new FakePromotionForUpdateDto().Generate();
        
        // Act
        fakePromotion.Update(updatedPromotion);

        // Assert
        fakePromotion.Should().BeEquivalentTo(updatedPromotion, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakePromotion = FakePromotion.Generate();
        var updatedPromotion = new FakePromotionForUpdateDto().Generate();
        fakePromotion.DomainEvents.Clear();
        
        // Act
        fakePromotion.Update(updatedPromotion);

        // Assert
        fakePromotion.DomainEvents.Count.Should().Be(1);
        fakePromotion.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PromotionUpdated));
    }
}