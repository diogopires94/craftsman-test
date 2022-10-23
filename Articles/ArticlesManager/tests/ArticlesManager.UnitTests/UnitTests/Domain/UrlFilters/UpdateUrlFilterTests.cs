namespace ArticlesManager.UnitTests.UnitTests.Domain.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateUrlFilterTests
{
    private readonly Faker _faker;

    public UpdateUrlFilterTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_urlFilter()
    {
        // Arrange
        var fakeUrlFilter = FakeUrlFilter.Generate();
        var updatedUrlFilter = new FakeUrlFilterForUpdateDto().Generate();
        
        // Act
        fakeUrlFilter.Update(updatedUrlFilter);

        // Assert
        fakeUrlFilter.Should().BeEquivalentTo(updatedUrlFilter, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUrlFilter = FakeUrlFilter.Generate();
        var updatedUrlFilter = new FakeUrlFilterForUpdateDto().Generate();
        fakeUrlFilter.DomainEvents.Clear();
        
        // Act
        fakeUrlFilter.Update(updatedUrlFilter);

        // Assert
        fakeUrlFilter.DomainEvents.Count.Should().Be(1);
        fakeUrlFilter.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UrlFilterUpdated));
    }
}