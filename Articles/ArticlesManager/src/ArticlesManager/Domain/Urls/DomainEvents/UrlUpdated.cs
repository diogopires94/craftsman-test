namespace ArticlesManager.Domain.Urls.DomainEvents;

public class UrlUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            