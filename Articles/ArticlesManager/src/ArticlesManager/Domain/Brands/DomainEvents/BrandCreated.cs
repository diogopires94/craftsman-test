namespace ArticlesManager.Domain.Brands.DomainEvents;

public class BrandCreated : DomainEvent
{
    public Brand Brand { get; set; } 
}
            