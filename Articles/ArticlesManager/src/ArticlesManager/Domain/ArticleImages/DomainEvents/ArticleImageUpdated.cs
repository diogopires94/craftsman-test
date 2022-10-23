namespace ArticlesManager.Domain.ArticleImages.DomainEvents;

public class ArticleImageUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            