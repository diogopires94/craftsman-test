namespace ArticlesManager.Domain.Families.DomainEvents;

public class FamilyCreated : DomainEvent
{
    public Family Family { get; set; } 
}
            