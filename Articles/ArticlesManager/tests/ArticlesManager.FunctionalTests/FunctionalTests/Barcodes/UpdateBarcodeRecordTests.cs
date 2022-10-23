namespace ArticlesManager.FunctionalTests.FunctionalTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateBarcodeRecordTests : TestBase
{
    [Test]
    public async Task put_barcode_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBarcode = FakeBarcode.Generate(new FakeBarcodeForCreationDto().Generate());
        var updatedBarcodeDto = new FakeBarcodeForUpdateDto { }.Generate();
        await InsertAsync(fakeBarcode);

        // Act
        var route = ApiRoutes.Barcodes.Put.Replace(ApiRoutes.Barcodes.Id, fakeBarcode.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedBarcodeDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}