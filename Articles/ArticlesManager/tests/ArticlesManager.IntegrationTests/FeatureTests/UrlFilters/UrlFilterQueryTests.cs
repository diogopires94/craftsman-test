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

public class UrlFilterQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_urlfilter_with_accurate_props()
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

        // Act
        var query = new GetUrlFilter.UrlFilterQuery(fakeUrlFilterOne.Id);
        var urlFilter = await SendAsync(query);

        // Assert
        urlFilter.Should().BeEquivalentTo(fakeUrlFilterOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_urlfilter_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetUrlFilter.UrlFilterQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}