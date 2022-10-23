namespace ArticlesManager.FunctionalTests.FunctionalTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBarcodeListTests : TestBase
{
    [Test]
    public async Task get_barcode_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Barcodes.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}