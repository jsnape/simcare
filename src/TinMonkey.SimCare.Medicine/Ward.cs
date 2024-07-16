using System.Diagnostics;
using TinMonkey.SimCare.Medicine.Domain;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name} Dept {Department}")]
public class Ward(string Name, Department Department) : Entity, ILocation
{
    private readonly Dictionary<string, Bed> beds = [];

    public string Name { get; } = Name;

    public Department Department { get; } = Department;

    public int AvailableBeds => this.beds.Values.Count(b => b.CurrentPatient == null);

    internal Bed AdmitPatient(Patient patient)
    {
        // Find a spare bed with no patient in and admit the patient.
        var bed = this.beds.Values.FirstOrDefault(b => b.CurrentPatient == null)
            ?? throw new InvalidOperationException("No beds available in ward.");

        bed.CurrentPatient = patient;

        return bed;
    }

    internal Bed CreateBed(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var bed = new Bed(name);
        this.beds.Add(name, bed);

        return bed;
    }
}
