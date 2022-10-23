namespace ArticlesManager.IntegrationTests.FeatureTests.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Barcodes.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class AddBarcodeCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_barcode_to_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBarcodeOne = new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate();

        // Act
        var command = new AddBarcode.AddBarcodeCommand(fakeBarcodeOne);
        var barcodeReturned = await SendAsync(command);
        var barcodeCreated = await ExecuteDbContextAsync(db => db.Barcodes
            .FirstOrDefaultAsync(b => b.Id == barcodeReturned.Id));

        // Assert
        barcodeReturned.Should().BeEquivalentTo(fakeBarcodeOne, options =>
            options.ExcludingMissingMembers());
        barcodeCreated.Should().BeEquivalentTo(fakeBarcodeOne, options =>
            options.ExcludingMissingMembers());
    }
}