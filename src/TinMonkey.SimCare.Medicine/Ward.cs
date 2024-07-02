using System.Diagnostics;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name} Dept {Department}")]
public class Ward(string Name, Department Department) : ILocation
{
    private readonly List<Bed> beds = [];

    public string Name { get; } = Name;

    public Department Department { get; } = Department;

    public Bed CreateBed(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);

        var bed = new Bed(name, this);
        this.beds.Add(bed);

        return bed;
    }
}
