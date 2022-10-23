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

public class SizeTableLineQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_sizetableline_with_accurate_props()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTableOne);

        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate());
        await InsertAsync(fakeSizeTableLineOne);

        // Act
        var query = new GetSizeTableLine.SizeTableLineQuery(fakeSizeTableLineOne.Id);
        var sizeTableLine = await SendAsync(query);

        // Assert
        sizeTableLine.Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_sizetableline_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetSizeTableLine.SizeTableLineQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}