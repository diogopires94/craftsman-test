namespace ArticlesManager.UnitTests.UnitTests.Domain.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateSubFamilyTests
{
    private readonly Faker _faker;

    public UpdateSubFamilyTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_subFamily()
    {
        // Arrange
        var fakeSubFamily = FakeSubFamily.Generate();
        var updatedSubFamily = new FakeSubFamilyForUpdateDto().Generate();
        
        // Act
        fakeSubFamily.Update(updatedSubFamily);

        // Assert
        fakeSubFamily.Should().BeEquivalentTo(updatedSubFamily, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeSubFamily = FakeSubFamily.Generate();
        var updatedSubFamily = new FakeSubFamilyForUpdateDto().Generate();
        fakeSubFamily.DomainEvents.Clear();
        
        // Act
        fakeSubFamily.Update(updatedSubFamily);

        // Assert
        fakeSubFamily.DomainEvents.Count.Should().Be(1);
        fakeSubFamily.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SubFamilyUpdated));
    }
}