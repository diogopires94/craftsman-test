namespace ArticlesManager.Domain.Barcodes.DomainEvents;

public class BarcodeCreated : DomainEvent
{
    public Barcode Barcode { get; set; } 
}
            