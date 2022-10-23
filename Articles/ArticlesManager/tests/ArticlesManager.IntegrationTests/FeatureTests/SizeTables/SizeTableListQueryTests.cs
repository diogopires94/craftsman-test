namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTables;

using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTables.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

public class SizeTableListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_sizetable_list()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    await InsertAsync(fakeSubFamilyOne, fakeSubFamilyTwo);

        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyOne.Id)
            .Generate());
        var fakeSizeTableTwo = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyTwo.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyTwo.Id)
            .Generate());
        var queryParameters = new SizeTableParametersDto();

        await InsertAsync(fakeSizeTableOne, fakeSizeTableTwo);

        // Act
        var query = new GetSizeTableList.SizeTableListQuery(queryParameters);
        var sizeTables = await SendAsync(query);

        // Assert
        sizeTables.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}