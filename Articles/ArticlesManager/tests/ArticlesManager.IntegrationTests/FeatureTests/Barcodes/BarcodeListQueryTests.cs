namespace ArticlesManager.IntegrationTests.FeatureTests.Barcodes;

using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Barcodes.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class BarcodeListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_barcode_list()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    await InsertAsync(fakeArticleOne, fakeArticleTwo);

        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleTwo.Id)
            .Generate());
        var queryParameters = new BarcodeParametersDto();

        await InsertAsync(fakeBarcodeOne, fakeBarcodeTwo);

        // Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var barcodes = await SendAsync(query);

        // Assert
        barcodes.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}