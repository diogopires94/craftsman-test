namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSizeTableListTests : TestBase
{
    [Test]
    public async Task get_sizetable_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.SizeTables.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}