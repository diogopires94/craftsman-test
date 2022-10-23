namespace ArticlesManager.UnitTests.UnitTests.Domain.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateSubFamilyTests
{
    private readonly Faker _faker;

    public CreateSubFamilyTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_subFamily()
    {
        // Arrange + Act
        var fakeSubFamily = FakeSubFamily.Generate();

        // Assert
        fakeSubFamily.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeSubFamily = FakeSubFamily.Generate();

        // Assert
        fakeSubFamily.DomainEvents.Count.Should().Be(1);
        fakeSubFamily.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SubFamilyCreated));
    }
}