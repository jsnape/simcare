namespace TinMonkey.Domain.Core;

public interface IHandles<T> where T : IDomainEvent
{
    void Handle(T args);

    Task HandleAsync(T args);
}
