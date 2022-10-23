namespace ArticlesManager.Domain.SubFamilies.DomainEvents;

public class SubFamilyCreated : DomainEvent
{
    public SubFamily SubFamily { get; set; } 
}
            