namespace ArticlesManager.UnitTests.UnitTests.Domain.Barcodes;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateBarcodeTests
{
    private readonly Faker _faker;

    public UpdateBarcodeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_barcode()
    {
        // Arrange
        var fakeBarcode = FakeBarcode.Generate();
        var updatedBarcode = new FakeBarcodeForUpdateDto().Generate();
        
        // Act
        fakeBarcode.Update(updatedBarcode);

        // Assert
        fakeBarcode.Should().BeEquivalentTo(updatedBarcode, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeBarcode = FakeBarcode.Generate();
        var updatedBarcode = new FakeBarcodeForUpdateDto().Generate();
        fakeBarcode.DomainEvents.Clear();
        
        // Act
        fakeBarcode.Update(updatedBarcode);

        // Assert
        fakeBarcode.DomainEvents.Count.Should().Be(1);
        fakeBarcode.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(BarcodeUpdated));
    }
}