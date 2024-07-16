namespace TinMonkey.Domain.Core;

public abstract class Entity
{
    public virtual Guid Id { get; protected set; } = Guid.NewGuid();
}
