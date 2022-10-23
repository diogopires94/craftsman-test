namespace ArticlesManager.UnitTests.UnitTests.Domain.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateBarcodeTests
{
    private readonly Faker _faker;

    public CreateBarcodeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_barcode()
    {
        // Arrange + Act
        var fakeBarcode = FakeBarcode.Generate();

        // Assert
        fakeBarcode.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeBarcode = FakeBarcode.Generate();

        // Assert
        fakeBarcode.DomainEvents.Count.Should().Be(1);
        fakeBarcode.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(BarcodeCreated));
    }
}