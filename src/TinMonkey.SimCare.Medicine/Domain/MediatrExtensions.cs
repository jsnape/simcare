using MediatR;

namespace TinMonkey.SimCare.Medicine.Domain;

internal static class MediatrExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<AggregateRoot> roots)
    {
        var domainEvents = roots
            .SelectMany(r => r.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }

    public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
