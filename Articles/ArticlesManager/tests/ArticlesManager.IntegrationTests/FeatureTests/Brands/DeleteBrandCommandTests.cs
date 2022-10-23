namespace ArticlesManager.IntegrationTests.FeatureTests.Brands;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteBrandCommandTests : TestBase
{
    [Test]
    public async Task can_delete_brand_from_db()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);
        var brand = await ExecuteDbContextAsync(db => db.Brands
            .FirstOrDefaultAsync(b => b.Id == fakeBrandOne.Id));

        // Act
        var command = new DeleteBrand.DeleteBrandCommand(brand.Id);
        await SendAsync(command);
        var brandResponse = await ExecuteDbContextAsync(db => db.Brands.CountAsync(b => b.Id == brand.Id));

        // Assert
        brandResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_brand_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteBrand.DeleteBrandCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_brand_from_db()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);
        var brand = await ExecuteDbContextAsync(db => db.Brands
            .FirstOrDefaultAsync(b => b.Id == fakeBrandOne.Id));

        // Act
        var command = new DeleteBrand.DeleteBrandCommand(brand.Id);
        await SendAsync(command);
        var deletedBrand = await ExecuteDbContextAsync(db => db.Brands
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == brand.Id));

        // Assert
        deletedBrand?.IsDeleted.Should().BeTrue();
    }
}