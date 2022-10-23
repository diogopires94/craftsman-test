namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.Domain.SizeTableLines.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;

public class DeleteSizeTableLineCommandTests : TestBase
{
    [Test]
    public async Task can_delete_sizetableline_from_db()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTableOne);

        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate());
        await InsertAsync(fakeSizeTableLineOne);
        var sizeTableLine = await ExecuteDbContextAsync(db => db.SizeTableLines
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableLineOne.Id));

        // Act
        var command = new DeleteSizeTableLine.DeleteSizeTableLineCommand(sizeTableLine.Id);
        await SendAsync(command);
        var sizeTableLineResponse = await ExecuteDbContextAsync(db => db.SizeTableLines.CountAsync(s => s.Id == sizeTableLine.Id));

        // Assert
        sizeTableLineResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_sizetableline_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteSizeTableLine.DeleteSizeTableLineCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_sizetableline_from_db()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTableOne);

        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate());
        await InsertAsync(fakeSizeTableLineOne);
        var sizeTableLine = await ExecuteDbContextAsync(db => db.SizeTableLines
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableLineOne.Id));

        // Act
        var command = new DeleteSizeTableLine.DeleteSizeTableLineCommand(sizeTableLine.Id);
        await SendAsync(command);
        var deletedSizeTableLine = await ExecuteDbContextAsync(db => db.SizeTableLines
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == sizeTableLine.Id));

        // Assert
        deletedSizeTableLine?.IsDeleted.Should().BeTrue();
    }
}