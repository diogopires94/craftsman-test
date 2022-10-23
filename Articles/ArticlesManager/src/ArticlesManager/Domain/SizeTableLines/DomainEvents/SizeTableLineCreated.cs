namespace ArticlesManager.Domain.SizeTableLines.DomainEvents;

public class SizeTableLineCreated : DomainEvent
{
    public SizeTableLine SizeTableLine { get; set; } 
}
            