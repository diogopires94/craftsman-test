namespace ArticlesManager.UnitTests.UnitTests.Domain.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateFamilyTests
{
    private readonly Faker _faker;

    public UpdateFamilyTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_family()
    {
        // Arrange
        var fakeFamily = FakeFamily.Generate();
        var updatedFamily = new FakeFamilyForUpdateDto().Generate();
        
        // Act
        fakeFamily.Update(updatedFamily);

        // Assert
        fakeFamily.Should().BeEquivalentTo(updatedFamily, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeFamily = FakeFamily.Generate();
        var updatedFamily = new FakeFamilyForUpdateDto().Generate();
        fakeFamily.DomainEvents.Clear();
        
        // Act
        fakeFamily.Update(updatedFamily);

        // Assert
        fakeFamily.DomainEvents.Count.Should().Be(1);
        fakeFamily.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FamilyUpdated));
    }
}