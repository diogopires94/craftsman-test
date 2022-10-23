namespace ArticlesManager.Domain.Collections.DomainEvents;

public class CollectionCreated : DomainEvent
{
    public Collection Collection { get; set; } 
}
            