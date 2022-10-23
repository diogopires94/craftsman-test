namespace ArticlesManager.FunctionalTests.FunctionalTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteSizeTableTests : TestBase
{
    [Test]
    public async Task delete_sizetable_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeSizeTable = FakeSizeTable.Generate(new FakeSizeTableForCreationDto().Generate());
        await InsertAsync(fakeSizeTable);

        // Act
        var route = ApiRoutes.SizeTables.Delete.Replace(ApiRoutes.SizeTables.Id, fakeSizeTable.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}