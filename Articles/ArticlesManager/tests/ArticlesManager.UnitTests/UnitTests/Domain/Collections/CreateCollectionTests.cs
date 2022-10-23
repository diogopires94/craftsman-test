namespace ArticlesManager.UnitTests.UnitTests.Domain.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateCollectionTests
{
    private readonly Faker _faker;

    public CreateCollectionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_collection()
    {
        // Arrange + Act
        var fakeCollection = FakeCollection.Generate();

        // Assert
        fakeCollection.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeCollection = FakeCollection.Generate();

        // Assert
        fakeCollection.DomainEvents.Count.Should().Be(1);
        fakeCollection.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(CollectionCreated));
    }
}