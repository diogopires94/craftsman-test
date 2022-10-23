namespace ArticlesManager.FunctionalTests.FunctionalTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateBarcodeTests : TestBase
{
    [Test]
    public async Task create_barcode_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeBarcode = new FakeBarcodeForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Barcodes.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeBarcode);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}