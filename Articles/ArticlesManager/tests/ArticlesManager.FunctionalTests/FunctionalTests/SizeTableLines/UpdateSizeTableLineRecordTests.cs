namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateSizeTableLineRecordTests : TestBase
{
    [Test]
    public async Task put_sizetableline_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeSizeTableLine = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto().Generate());
        var updatedSizeTableLineDto = new FakeSizeTableLineForUpdateDto { }.Generate();
        await InsertAsync(fakeSizeTableLine);

        // Act
        var route = ApiRoutes.SizeTableLines.Put.Replace(ApiRoutes.SizeTableLines.Id, fakeSizeTableLine.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedSizeTableLineDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}