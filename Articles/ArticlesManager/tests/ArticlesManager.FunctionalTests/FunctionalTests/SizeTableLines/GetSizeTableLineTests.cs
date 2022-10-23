namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSizeTableLineTests : TestBase
{
    [Test]
    public async Task get_sizetableline_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeSizeTableLine = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto().Generate());
        await InsertAsync(fakeSizeTableLine);

        // Act
        var route = ApiRoutes.SizeTableLines.GetRecord.Replace(ApiRoutes.SizeTableLines.Id, fakeSizeTableLine.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}