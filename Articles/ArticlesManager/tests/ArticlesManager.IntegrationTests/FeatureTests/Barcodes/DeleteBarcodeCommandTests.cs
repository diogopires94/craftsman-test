namespace ArticlesManager.IntegrationTests.FeatureTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.Domain.Barcodes.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class DeleteBarcodeCommandTests : TestBase
{
    [Test]
    public async Task can_delete_barcode_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeBarcodeOne);
        var barcode = await ExecuteDbContextAsync(db => db.Barcodes
            .FirstOrDefaultAsync(b => b.Id == fakeBarcodeOne.Id));

        // Act
        var command = new DeleteBarcode.DeleteBarcodeCommand(barcode.Id);
        await SendAsync(command);
        var barcodeResponse = await ExecuteDbContextAsync(db => db.Barcodes.CountAsync(b => b.Id == barcode.Id));

        // Assert
        barcodeResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_barcode_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteBarcode.DeleteBarcodeCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_barcode_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeBarcodeOne);
        var barcode = await ExecuteDbContextAsync(db => db.Barcodes
            .FirstOrDefaultAsync(b => b.Id == fakeBarcodeOne.Id));

        // Act
        var command = new DeleteBarcode.DeleteBarcodeCommand(barcode.Id);
        await SendAsync(command);
        var deletedBarcode = await ExecuteDbContextAsync(db => db.Barcodes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == barcode.Id));

        // Assert
        deletedBarcode?.IsDeleted.Should().BeTrue();
    }
}