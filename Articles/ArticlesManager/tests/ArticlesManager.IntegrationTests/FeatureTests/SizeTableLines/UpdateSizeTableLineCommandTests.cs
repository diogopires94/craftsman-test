namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.Domain.SizeTableLines.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTableLines.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;

public class UpdateSizeTableLineCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_sizetableline_in_db()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTableOne);

        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate());
        var updatedSizeTableLineDto = new FakeSizeTableLineForUpdateDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate();
        await InsertAsync(fakeSizeTableLineOne);

        var sizeTableLine = await ExecuteDbContextAsync(db => db.SizeTableLines
            .FirstOrDefaultAsync(s => s.Id == fakeSizeTableLineOne.Id));
        var id = sizeTableLine.Id;

        // Act
        var command = new UpdateSizeTableLine.UpdateSizeTableLineCommand(id, updatedSizeTableLineDto);
        await SendAsync(command);
        var updatedSizeTableLine = await ExecuteDbContextAsync(db => db.SizeTableLines.FirstOrDefaultAsync(s => s.Id == id));

        // Assert
        updatedSizeTableLine.Should().BeEquivalentTo(updatedSizeTableLineDto, options =>
            options.ExcludingMissingMembers());
    }
}