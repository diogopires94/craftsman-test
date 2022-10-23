namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetSizeTableTests : TestBase
{
    [Test]
    public async Task get_sizetable_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeSizeTable = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTable);

        // Act
        var route = ApiRoutes.SizeTables.GetRecord.Replace(ApiRoutes.SizeTables.Id, fakeSizeTable.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}