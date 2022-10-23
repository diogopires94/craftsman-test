namespace ArticlesManager.Domain.UserCharts.DomainEvents;

public class UserChartCreated : DomainEvent
{
    public UserChart UserChart { get; set; } 
}
            