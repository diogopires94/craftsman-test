namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateSizeTableTests : TestBase
{
    [Test]
    public async Task create_sizetable_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeSizeTable = new FakeSizeTableForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.SizeTables.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeSizeTable);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}