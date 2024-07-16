using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

public class Medic(string Name) : AggregateRoot
{
    public string Name { get; } = Name;
}
