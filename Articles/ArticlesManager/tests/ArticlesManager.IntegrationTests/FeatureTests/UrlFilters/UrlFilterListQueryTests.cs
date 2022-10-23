namespace ArticlesManager.IntegrationTests.FeatureTests.UrlFilters;

using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.UrlFilters.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class UrlFilterListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_urlfilter_list()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
    var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
    await InsertAsync(fakeUrlOne, fakeUrlTwo);

        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    await InsertAsync(fakeSubFamilyOne, fakeSubFamilyTwo);

        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    await InsertAsync(fakeBrandOne, fakeBrandTwo);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    await InsertAsync(fakeCollectionOne, fakeCollectionTwo);

        var fakeUrlFilterOne = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto()
            .RuleFor(u => u.UrlId, _ => fakeUrlOne.Id)
            
            .RuleFor(u => u.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(u => u.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(u => u.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(u => u.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        var fakeUrlFilterTwo = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto()
            .RuleFor(u => u.UrlId, _ => fakeUrlTwo.Id)
            
            .RuleFor(u => u.FamilyId, _ => fakeFamilyTwo.Id)
            
            .RuleFor(u => u.SubFamilyId, _ => fakeSubFamilyTwo.Id)
            
            .RuleFor(u => u.BrandId, _ => fakeBrandTwo.Id)
            
            .RuleFor(u => u.CollectionId, _ => fakeCollectionTwo.Id)
            .Generate());
        var queryParameters = new UrlFilterParametersDto();

        await InsertAsync(fakeUrlFilterOne, fakeUrlFilterTwo);

        // Act
        var query = new GetUrlFilterList.UrlFilterListQuery(queryParameters);
        var urlFilters = await SendAsync(query);

        // Assert
        urlFilters.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}