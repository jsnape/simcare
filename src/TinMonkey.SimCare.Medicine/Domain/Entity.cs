namespace TinMonkey.SimCare.Medicine.Domain;

public abstract class Entity
{
    public virtual Guid Id { get; protected set; } = Guid.NewGuid();
}
