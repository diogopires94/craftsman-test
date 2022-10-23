namespace ArticlesManager.IntegrationTests.FeatureTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Brands.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateBrandCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_brand_in_db()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        var updatedBrandDto = new FakeBrandForUpdateDto().Generate();
        await InsertAsync(fakeBrandOne);

        var brand = await ExecuteDbContextAsync(db => db.Brands
            .FirstOrDefaultAsync(b => b.Id == fakeBrandOne.Id));
        var id = brand.Id;

        // Act
        var command = new UpdateBrand.UpdateBrandCommand(id, updatedBrandDto);
        await SendAsync(command);
        var updatedBrand = await ExecuteDbContextAsync(db => db.Brands.FirstOrDefaultAsync(b => b.Id == id));

        // Assert
        updatedBrand.Should().BeEquivalentTo(updatedBrandDto, options =>
            options.ExcludingMissingMembers());
    }
}