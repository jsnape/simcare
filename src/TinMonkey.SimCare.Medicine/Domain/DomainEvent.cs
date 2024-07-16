using MediatR;

namespace TinMonkey.SimCare.Medicine.Domain;

public class DomainEvent(Guid EventId) : INotification
{
    public DomainEvent()
        : this(Guid.NewGuid())
    {
    }

    public Guid EventId { get; } = EventId;
}
