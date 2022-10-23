namespace ArticlesManager.UnitTests.UnitTests.Domain.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateBrandTests
{
    private readonly Faker _faker;

    public UpdateBrandTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_brand()
    {
        // Arrange
        var fakeBrand = FakeBrand.Generate();
        var updatedBrand = new FakeBrandForUpdateDto().Generate();
        
        // Act
        fakeBrand.Update(updatedBrand);

        // Assert
        fakeBrand.Should().BeEquivalentTo(updatedBrand, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeBrand = FakeBrand.Generate();
        var updatedBrand = new FakeBrandForUpdateDto().Generate();
        fakeBrand.DomainEvents.Clear();
        
        // Act
        fakeBrand.Update(updatedBrand);

        // Assert
        fakeBrand.DomainEvents.Count.Should().Be(1);
        fakeBrand.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(BrandUpdated));
    }
}