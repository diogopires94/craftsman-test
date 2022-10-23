namespace ArticlesManager.UnitTests.UnitTests.Domain.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateUrlTests
{
    private readonly Faker _faker;

    public UpdateUrlTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_url()
    {
        // Arrange
        var fakeUrl = FakeUrl.Generate();
        var updatedUrl = new FakeUrlForUpdateDto().Generate();
        
        // Act
        fakeUrl.Update(updatedUrl);

        // Assert
        fakeUrl.Should().BeEquivalentTo(updatedUrl, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUrl = FakeUrl.Generate();
        var updatedUrl = new FakeUrlForUpdateDto().Generate();
        fakeUrl.DomainEvents.Clear();
        
        // Act
        fakeUrl.Update(updatedUrl);

        // Assert
        fakeUrl.DomainEvents.Count.Should().Be(1);
        fakeUrl.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UrlUpdated));
    }
}