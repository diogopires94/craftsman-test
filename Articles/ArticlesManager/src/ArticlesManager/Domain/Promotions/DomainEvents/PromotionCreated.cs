namespace ArticlesManager.Domain.Promotions.DomainEvents;

public class PromotionCreated : DomainEvent
{
    public Promotion Promotion { get; set; } 
}
            