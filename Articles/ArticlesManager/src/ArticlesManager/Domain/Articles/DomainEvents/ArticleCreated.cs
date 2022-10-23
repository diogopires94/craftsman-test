namespace ArticlesManager.Domain.Articles.DomainEvents;

public class ArticleCreated : DomainEvent
{
    public Article Article { get; set; } 
}
            