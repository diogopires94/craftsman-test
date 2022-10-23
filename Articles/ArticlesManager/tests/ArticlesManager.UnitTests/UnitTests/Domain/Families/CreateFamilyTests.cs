namespace ArticlesManager.UnitTests.UnitTests.Domain.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateFamilyTests
{
    private readonly Faker _faker;

    public CreateFamilyTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_family()
    {
        // Arrange + Act
        var fakeFamily = FakeFamily.Generate();

        // Assert
        fakeFamily.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeFamily = FakeFamily.Generate();

        // Assert
        fakeFamily.DomainEvents.Count.Should().Be(1);
        fakeFamily.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FamilyCreated));
    }
}