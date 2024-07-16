using System.Diagnostics;
using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name}")]
public class Department(string Name) : AggregateRoot, ILocation
{
    private readonly Dictionary<string, Ward> wards = [];

    public string Name { get; } = Name;

    public Ward CreateWard(string? name)
    {
        // Blank ward name should default to the department name.
        var wardName = name ?? this.Name;

        var ward = new Ward(wardName, this);
        this.wards.Add(wardName, ward);

        var domainEvent = new WardCreatedDomainEvent(ward.Id, ward, this);
        this.AddDomainEvent(domainEvent);

        return ward;
    }

    public Bed CreateBed(string name, Ward ward)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(ward);

        var bed = ward.CreateBed(name);

        var domainEvent = new BedCreatedDomainEvent(ward.Id, bed, ward, this);
        this.AddDomainEvent(domainEvent);

        return bed;
    }

    public Bed AdmitPatient(Patient patient)
    {
        var ward = this.wards.Values.FirstOrDefault(w => w.AvailableBeds > 0)
            ?? throw new InvalidOperationException("No beds available in department.");

        return this.AdmitPatient(patient, ward);
    }

    public Bed AdmitPatient(Patient patient, Ward ward)
    {
        ArgumentNullException.ThrowIfNull(patient);
        ArgumentNullException.ThrowIfNull(ward);

        var bed = ward.AdmitPatient(patient);

        var domainEvent = new PatientAdmittedDomainEvent(patient.Id, patient, ward, bed);
        this.AddDomainEvent(domainEvent);

        return bed;
    }

    ////public Bed DischargePatient(Patient patient)
    ////{
    ////    ArgumentNullException.ThrowIfNull(patient);

    ////    var ward = this.wards.Values.FirstOrDefault(w => w.Beds.Values.Any(b => b.CurrentPatient == patient))
    ////        ?? throw new InvalidOperationException("Patient not found in department.");

    ////    return this.DischargePatient(patient, ward);
    ////}
}
