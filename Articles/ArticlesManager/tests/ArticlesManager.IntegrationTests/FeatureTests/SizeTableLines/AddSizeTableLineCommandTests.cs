namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.SizeTableLines.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;

public class AddSizeTableLineCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_sizetableline_to_db()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTableOne);

        var fakeSizeTableLineOne = new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate();

        // Act
        var command = new AddSizeTableLine.AddSizeTableLineCommand(fakeSizeTableLineOne);
        var sizeTableLineReturned = await SendAsync(command);
        var sizeTableLineCreated = await ExecuteDbContextAsync(db => db.SizeTableLines
            .FirstOrDefaultAsync(s => s.Id == sizeTableLineReturned.Id));

        // Assert
        sizeTableLineReturned.Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
            options.ExcludingMissingMembers());
        sizeTableLineCreated.Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
            options.ExcludingMissingMembers());
    }
}