namespace ArticlesManager.UnitTests.UnitTests.Domain.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateUrlTests
{
    private readonly Faker _faker;

    public CreateUrlTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_url()
    {
        // Arrange + Act
        var fakeUrl = FakeUrl.Generate();

        // Assert
        fakeUrl.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeUrl = FakeUrl.Generate();

        // Assert
        fakeUrl.DomainEvents.Count.Should().Be(1);
        fakeUrl.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UrlCreated));
    }
}