namespace ArticlesManager.IntegrationTests.FeatureTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.Domain.Barcodes.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Barcodes.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class UpdateBarcodeCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_barcode_in_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var updatedBarcodeDto = new FakeBarcodeForUpdateDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate();
        await InsertAsync(fakeBarcodeOne);

        var barcode = await ExecuteDbContextAsync(db => db.Barcodes
            .FirstOrDefaultAsync(b => b.Id == fakeBarcodeOne.Id));
        var id = barcode.Id;

        // Act
        var command = new UpdateBarcode.UpdateBarcodeCommand(id, updatedBarcodeDto);
        await SendAsync(command);
        var updatedBarcode = await ExecuteDbContextAsync(db => db.Barcodes.FirstOrDefaultAsync(b => b.Id == id));

        // Assert
        updatedBarcode.Should().BeEquivalentTo(updatedBarcodeDto, options =>
            options.ExcludingMissingMembers());
    }
}