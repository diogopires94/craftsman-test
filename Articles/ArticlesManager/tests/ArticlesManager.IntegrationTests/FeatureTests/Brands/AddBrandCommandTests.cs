namespace ArticlesManager.IntegrationTests.FeatureTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Brands.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddBrandCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_brand_to_db()
    {
        // Arrange
        var fakeBrandOne = new FakeBrandForCreationDto().Generate();

        // Act
        var command = new AddBrand.AddBrandCommand(fakeBrandOne);
        var brandReturned = await SendAsync(command);
        var brandCreated = await ExecuteDbContextAsync(db => db.Brands
            .FirstOrDefaultAsync(b => b.Id == brandReturned.Id));

        // Assert
        brandReturned.Should().BeEquivalentTo(fakeBrandOne, options =>
            options.ExcludingMissingMembers());
        brandCreated.Should().BeEquivalentTo(fakeBrandOne, options =>
            options.ExcludingMissingMembers());
    }
}