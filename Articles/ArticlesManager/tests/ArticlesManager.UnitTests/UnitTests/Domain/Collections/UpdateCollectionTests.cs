namespace ArticlesManager.UnitTests.UnitTests.Domain.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateCollectionTests
{
    private readonly Faker _faker;

    public UpdateCollectionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_collection()
    {
        // Arrange
        var fakeCollection = FakeCollection.Generate();
        var updatedCollection = new FakeCollectionForUpdateDto().Generate();
        
        // Act
        fakeCollection.Update(updatedCollection);

        // Assert
        fakeCollection.Should().BeEquivalentTo(updatedCollection, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeCollection = FakeCollection.Generate();
        var updatedCollection = new FakeCollectionForUpdateDto().Generate();
        fakeCollection.DomainEvents.Clear();
        
        // Act
        fakeCollection.Update(updatedCollection);

        // Assert
        fakeCollection.DomainEvents.Count.Should().Be(1);
        fakeCollection.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(CollectionUpdated));
    }
}