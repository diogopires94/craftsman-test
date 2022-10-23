namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSizeTableLineListTests : TestBase
{
    [Test]
    public async Task get_sizetableline_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.SizeTableLines.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}