namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTables.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

public class UpdateSizeTableCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_sizetable_in_db()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyOne.Id)
            .Generate());
        var updatedSizeTableDto = new FakeSizeTableForUpdateDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyOne.Id)
            .Generate();
        await InsertAsync(fakeSizeTableOne);

        var sizeTable = await ExecuteDbContextAsync(db => db.SizeTables
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableOne.Id));
        var id = sizeTable.Id;

        // Act
        var command = new UpdateSizeTable.UpdateSizeTableCommand(id, updatedSizeTableDto);
        await SendAsync(command);
        var updatedSizeTable = await ExecuteDbContextAsync(db => db.SizeTables.FirstOrDefaultAsync(s => s.Id == id));

        // Assert
        updatedSizeTable.Should().BeEquivalentTo(updatedSizeTableDto, options =>
            options.ExcludingMissingMembers());
    }
}