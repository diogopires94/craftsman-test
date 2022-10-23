namespace ArticlesManager.Domain.HomePageHighlights.DomainEvents;

public class HomePageHighlightCreated : DomainEvent
{
    public HomePageHighlight HomePageHighlight { get; set; } 
}
            