namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

public class DeleteSizeTableCommandTests : TestBase
{
    [Test]
    public async Task can_delete_sizetable_from_db()
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
        await InsertAsync(fakeSizeTableOne);
        var sizeTable = await ExecuteDbContextAsync(db => db.SizeTables
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableOne.Id));

        // Act
        var command = new DeleteSizeTable.DeleteSizeTableCommand(sizeTable.Id);
        await SendAsync(command);
        var sizeTableResponse = await ExecuteDbContextAsync(db => db.SizeTables.CountAsync(s => s.Id == sizeTable.Id));

        // Assert
        sizeTableResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_sizetable_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteSizeTable.DeleteSizeTableCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_sizetable_from_db()
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
        await InsertAsync(fakeSizeTableOne);
        var sizeTable = await ExecuteDbContextAsync(db => db.SizeTables
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableOne.Id));

        // Act
        var command = new DeleteSizeTable.DeleteSizeTableCommand(sizeTable.Id);
        await SendAsync(command);
        var deletedSizeTable = await ExecuteDbContextAsync(db => db.SizeTables
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == sizeTable.Id));

        // Assert
        deletedSizeTable?.IsDeleted.Should().BeTrue();
    }
}