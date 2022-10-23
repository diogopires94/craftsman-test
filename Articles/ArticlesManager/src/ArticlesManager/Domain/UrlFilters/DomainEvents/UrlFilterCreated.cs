namespace ArticlesManager.Domain.UrlFilters.DomainEvents;

public class UrlFilterCreated : DomainEvent
{
    public UrlFilter UrlFilter { get; set; } 
}
            