namespace TinMonkey.SimCare.Medicine.Domain;

public abstract class AggregateRoot : Entity
{
    private List<DomainEvent>? domainEvents;

    public IEnumerable<DomainEvent> DomainEvents =>
        this.domainEvents?.AsReadOnly() ?? Enumerable.Empty<DomainEvent>();

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        this.domainEvents = this.domainEvents ?? [];
        this.domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent) => this.domainEvents?.Remove(domainEvent);

    public void ClearDomainEvents() => this.domainEvents?.Clear();
}
