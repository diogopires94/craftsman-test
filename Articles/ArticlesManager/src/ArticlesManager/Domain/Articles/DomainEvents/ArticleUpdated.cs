namespace ArticlesManager.Domain.Articles.DomainEvents;

public class ArticleUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            