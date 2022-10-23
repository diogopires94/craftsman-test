namespace ArticlesManager.UnitTests.UnitTests.Domain.HomePageHighlights;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateHomePageHighlightTests
{
    private readonly Faker _faker;

    public UpdateHomePageHighlightTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_homePageHighlight()
    {
        // Arrange
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate();
        var updatedHomePageHighlight = new FakeHomePageHighlightForUpdateDto().Generate();
        
        // Act
        fakeHomePageHighlight.Update(updatedHomePageHighlight);

        // Assert
        fakeHomePageHighlight.Should().BeEquivalentTo(updatedHomePageHighlight, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeHomePageHighlight = FakeHomePageHighlight.Generate();
        var updatedHomePageHighlight = new FakeHomePageHighlightForUpdateDto().Generate();
        fakeHomePageHighlight.DomainEvents.Clear();
        
        // Act
        fakeHomePageHighlight.Update(updatedHomePageHighlight);

        // Assert
        fakeHomePageHighlight.DomainEvents.Count.Should().Be(1);
        fakeHomePageHighlight.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(HomePageHighlightUpdated));
    }
}