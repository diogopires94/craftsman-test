namespace ArticlesManager.FunctionalTests.FunctionalTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteBarcodeTests : TestBase
{
    [Test]
    public async Task delete_barcode_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBarcode = FakeBarcode.Generate(new FakeBarcodeForCreationDto().Generate());
        await InsertAsync(fakeBarcode);

        // Act
        var route = ApiRoutes.Barcodes.Delete.Replace(ApiRoutes.Barcodes.Id, fakeBarcode.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}