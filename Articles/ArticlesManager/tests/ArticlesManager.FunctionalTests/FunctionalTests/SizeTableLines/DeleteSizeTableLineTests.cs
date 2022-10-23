namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteSizeTableLineTests : TestBase
{
    [Test]
    public async Task delete_sizetableline_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeSizeTableLine = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto().Generate());
        await InsertAsync(fakeSizeTableLine);

        // Act
        var route = ApiRoutes.SizeTableLines.Delete.Replace(ApiRoutes.SizeTableLines.Id, fakeSizeTableLine.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}