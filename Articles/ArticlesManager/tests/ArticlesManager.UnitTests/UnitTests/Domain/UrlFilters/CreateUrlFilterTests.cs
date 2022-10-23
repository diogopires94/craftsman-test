namespace ArticlesManager.UnitTests.UnitTests.Domain.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateUrlFilterTests
{
    private readonly Faker _faker;

    public CreateUrlFilterTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_urlFilter()
    {
        // Arrange + Act
        var fakeUrlFilter = FakeUrlFilter.Generate();

        // Assert
        fakeUrlFilter.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeUrlFilter = FakeUrlFilter.Generate();

        // Assert
        fakeUrlFilter.DomainEvents.Count.Should().Be(1);
        fakeUrlFilter.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UrlFilterCreated));
    }
}