namespace ArticlesManager.Domain.SizeTables.DomainEvents;

public class SizeTableCreated : DomainEvent
{
    public SizeTable SizeTable { get; set; } 
}
            