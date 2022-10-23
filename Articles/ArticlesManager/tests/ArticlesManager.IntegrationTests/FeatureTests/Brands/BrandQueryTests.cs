namespace ArticlesManager.IntegrationTests.FeatureTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class BrandQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_brand_with_accurate_props()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);

        // Act
        var query = new GetBrand.BrandQuery(fakeBrandOne.Id);
        var brand = await SendAsync(query);

        // Assert
        brand.Should().BeEquivalentTo(fakeBrandOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_brand_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetBrand.BrandQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}