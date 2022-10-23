namespace ArticlesManager.UnitTests.UnitTests.Domain.Promotions;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreatePromotionTests
{
    private readonly Faker _faker;

    public CreatePromotionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_promotion()
    {
        // Arrange + Act
        var fakePromotion = FakePromotion.Generate();

        // Assert
        fakePromotion.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakePromotion = FakePromotion.Generate();

        // Assert
        fakePromotion.DomainEvents.Count.Should().Be(1);
        fakePromotion.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PromotionCreated));
    }
}