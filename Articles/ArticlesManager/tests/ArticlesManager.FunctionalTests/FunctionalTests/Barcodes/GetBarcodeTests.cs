namespace ArticlesManager.FunctionalTests.FunctionalTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBarcodeTests : TestBase
{
    [Test]
    public async Task get_barcode_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeBarcode = FakeBarcode.Generate(new FakeBarcodeForCreationDto().Generate());
        await InsertAsync(fakeBarcode);

        // Act
        var route = ApiRoutes.Barcodes.GetRecord.Replace(ApiRoutes.Barcodes.Id, fakeBarcode.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}