namespace ArticlesManager.IntegrationTests.FeatureTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.UrlFilters.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class AddUrlFilterCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_urlfilter_to_db()
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

        var fakeUrlFilterOne = new FakeUrlFilterForCreationDto()
            .RuleFor(u => u.UrlId, _ => fakeUrlOne.Id)
            
            .RuleFor(u => u.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(u => u.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(u => u.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(u => u.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();

        // Act
        var command = new AddUrlFilter.AddUrlFilterCommand(fakeUrlFilterOne);
        var urlFilterReturned = await SendAsync(command);
        var urlFilterCreated = await ExecuteDbContextAsync(db => db.UrlFilters
            .FirstOrDefaultAsync(u => u.Id == urlFilterReturned.Id));

        // Assert
        urlFilterReturned.Should().BeEquivalentTo(fakeUrlFilterOne, options =>
            options.ExcludingMissingMembers());
        urlFilterCreated.Should().BeEquivalentTo(fakeUrlFilterOne, options =>
            options.ExcludingMissingMembers());
    }
}