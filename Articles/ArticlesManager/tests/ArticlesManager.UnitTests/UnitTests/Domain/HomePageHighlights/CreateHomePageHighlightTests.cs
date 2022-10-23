namespace ArticlesManager.UnitTests.UnitTests.Domain.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateHomePageHighlightTests
{
    private readonly Faker _faker;

    public CreateHomePageHighlightTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_homePageHighlight()
    {
        // Arrange + Act
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate();

        // Assert
        fakeHomePageHighlight.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate();

        // Assert
        fakeHomePageHighlight.DomainEvents.Count.Should().Be(1);
        fakeHomePageHighlight.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(HomePageHighlightCreated));
    }
}