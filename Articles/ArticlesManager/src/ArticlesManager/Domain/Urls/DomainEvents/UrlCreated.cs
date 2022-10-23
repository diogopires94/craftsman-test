namespace ArticlesManager.Domain.Urls.DomainEvents;

public class UrlCreated : DomainEvent
{
    public Url Url { get; set; } 
}
            