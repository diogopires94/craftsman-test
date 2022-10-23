namespace ArticlesManager.UnitTests.UnitTests.Domain.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateBrandTests
{
    private readonly Faker _faker;

    public CreateBrandTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_brand()
    {
        // Arrange + Act
        var fakeBrand = FakeBrand.Generate();

        // Assert
        fakeBrand.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeBrand = FakeBrand.Generate();

        // Assert
        fakeBrand.DomainEvents.Count.Should().Be(1);
        fakeBrand.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(BrandCreated));
    }
}