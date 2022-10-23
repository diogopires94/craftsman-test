namespace ArticlesManager.Domain.Barcodes.DomainEvents;

public class BarcodeUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            