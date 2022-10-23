namespace ArticlesManager.IntegrationTests.FeatureTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.Domain.UrlFilters.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.UrlFilters.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class UpdateUrlFilterCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_urlfilter_in_db()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        await InsertAsync(fakeUrlOne);

        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);

        var fakeUrlFilterOne = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto()
            .RuleFor(u => u.UrlId, _ => fakeUrlOne.Id)
            
            .RuleFor(u => u.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(u => u.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(u => u.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(u => u.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        var updatedUrlFilterDto = new FakeUrlFilterForUpdateDto()
            .RuleFor(u => u.UrlId, _ => fakeUrlOne.Id)
            
            .RuleFor(u => u.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(u => u.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(u => u.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(u => u.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();
        await InsertAsync(fakeUrlFilterOne);

        var urlFilter = await ExecuteDbContextAsync(db => db.UrlFilters
            .FirstOrDefaultAsync(u => u.Id == fakeUrlFilterOne.Id));
        var id = urlFilter.Id;

        // Act
        var command = new UpdateUrlFilter.UpdateUrlFilterCommand(id, updatedUrlFilterDto);
        await SendAsync(command);
        var updatedUrlFilter = await ExecuteDbContextAsync(db => db.UrlFilters.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUrlFilter.Should().BeEquivalentTo(updatedUrlFilterDto, options =>
            options.ExcludingMissingMembers());
    }
}