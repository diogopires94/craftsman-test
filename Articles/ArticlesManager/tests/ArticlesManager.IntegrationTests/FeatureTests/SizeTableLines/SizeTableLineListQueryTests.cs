namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTableLines;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTableLines.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;

public class SizeTableLineListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_sizetableline_list()
    {
        // Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
    var fakeSizeTableTwo = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
    await InsertAsync(fakeSizeTableOne, fakeSizeTableTwo);

        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableOne.Id)
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.SizeTableId, _ => fakeSizeTableTwo.Id)
            .Generate());
        var queryParameters = new SizeTableLineParametersDto();

        await InsertAsync(fakeSizeTableLineOne, fakeSizeTableLineTwo);

        // Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var sizeTableLines = await SendAsync(query);

        // Assert
        sizeTableLines.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}