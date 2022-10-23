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

public class BarcodeQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_barcode_with_accurate_props()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeBarcodeOne);

        // Act
        var query = new GetBarcode.BarcodeQuery(fakeBarcodeOne.Id);
        var barcode = await SendAsync(query);

        // Assert
        barcode.Should().BeEquivalentTo(fakeBarcodeOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_barcode_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetBarcode.BarcodeQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}