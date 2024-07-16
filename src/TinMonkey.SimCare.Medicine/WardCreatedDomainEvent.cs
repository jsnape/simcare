using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

internal class WardCreatedDomainEvent(Guid Id, Ward Ward, Department Department) : DomainEvent
{
    public Guid Id { get; } = Id;

    public Ward Ward { get; } = Ward;

    public Department Department { get; } = Department;
}
