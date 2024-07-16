using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

internal class BedCreatedDomainEvent(Guid Id, Bed Bed, Ward Ward, Department Department) : DomainEvent
{
    public Guid Id { get; } = Id;

    public Bed Bed { get; } = Bed;

    public Ward Ward { get; } = Ward;

    public Department Department { get; } = Department;
}
