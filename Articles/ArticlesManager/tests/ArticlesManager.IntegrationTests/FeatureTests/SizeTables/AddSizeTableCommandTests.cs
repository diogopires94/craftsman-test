namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.SizeTables.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

public class AddSizeTableCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_sizetable_to_db()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        var fakeSizeTableOne = new FakeSizeTableForCreationDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyOne.Id)
            .Generate();

        // Act
        var command = new AddSizeTable.AddSizeTableCommand(fakeSizeTableOne);
        var sizeTableReturned = await SendAsync(command);
        var sizeTableCreated = await ExecuteDbContextAsync(db => db.SizeTables
            .FirstOrDefaultAsync(s => s.Id == sizeTableReturned.Id));

        // Assert
        sizeTableReturned.Should().BeEquivalentTo(fakeSizeTableOne, options =>
            options.ExcludingMissingMembers());
        sizeTableCreated.Should().BeEquivalentTo(fakeSizeTableOne, options =>
            options.ExcludingMissingMembers());
    }
}