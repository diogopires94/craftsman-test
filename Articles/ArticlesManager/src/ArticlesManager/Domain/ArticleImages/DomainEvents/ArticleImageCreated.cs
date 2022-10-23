namespace ArticlesManager.Domain.ArticleImages.DomainEvents;

public class ArticleImageCreated : DomainEvent
{
    public ArticleImage ArticleImage { get; set; } 
}
            