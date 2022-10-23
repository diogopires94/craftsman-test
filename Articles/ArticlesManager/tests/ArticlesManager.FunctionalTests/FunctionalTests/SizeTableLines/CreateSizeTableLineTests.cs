namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateSizeTableLineTests : TestBase
{
    [Test]
    public async Task create_sizetableline_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeSizeTableLine = new FakeSizeTableLineForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.SizeTableLines.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeSizeTableLine);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}