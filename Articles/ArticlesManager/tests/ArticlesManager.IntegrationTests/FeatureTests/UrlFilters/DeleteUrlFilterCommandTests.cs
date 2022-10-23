namespace ArticlesManager.IntegrationTests.FeatureTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.Domain.UrlFilters.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class DeleteUrlFilterCommandTests : TestBase
{
    [Test]
    public async Task can_delete_urlfilter_from_db()
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
        await InsertAsync(fakeUrlFilterOne);
        var urlFilter = await ExecuteDbContextAsync(db => db.UrlFilters
            .FirstOrDefaultAsync(u => u.Id == fakeUrlFilterOne.Id));

        // Act
        var command = new DeleteUrlFilter.DeleteUrlFilterCommand(urlFilter.Id);
        await SendAsync(command);
        var urlFilterResponse = await ExecuteDbContextAsync(db => db.UrlFilters.CountAsync(u => u.Id == urlFilter.Id));

        // Assert
        urlFilterResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_urlfilter_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteUrlFilter.DeleteUrlFilterCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_urlfilter_from_db()
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
        await InsertAsync(fakeUrlFilterOne);
        var urlFilter = await ExecuteDbContextAsync(db => db.UrlFilters
            .FirstOrDefaultAsync(u => u.Id == fakeUrlFilterOne.Id));

        // Act
        var command = new DeleteUrlFilter.DeleteUrlFilterCommand(urlFilter.Id);
        await SendAsync(command);
        var deletedUrlFilter = await ExecuteDbContextAsync(db => db.UrlFilters
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == urlFilter.Id));

        // Assert
        deletedUrlFilter?.IsDeleted.Should().BeTrue();
    }
}