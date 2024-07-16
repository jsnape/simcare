using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

internal class PatientAdmittedDomainEvent(Guid Id, Patient Patient, Ward Ward, Bed? Bed) : DomainEvent
{
    public Guid Id { get; } = Id;

    public Patient Patient { get; } = Patient;

    public Ward Ward { get; } = Ward;

    public Bed? Bed { get; } = Bed;
}
