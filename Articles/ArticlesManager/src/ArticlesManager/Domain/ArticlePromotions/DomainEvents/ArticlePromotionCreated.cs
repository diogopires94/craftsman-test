namespace ArticlesManager.Domain.ArticlePromotions.DomainEvents;

public class ArticlePromotionCreated : DomainEvent
{
    public ArticlePromotion ArticlePromotion { get; set; } 
}
            