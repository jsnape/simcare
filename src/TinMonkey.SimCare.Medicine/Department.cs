using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name}")]
public class Department(string Name) : ILocation
{
    private readonly List<Ward> wards = [];

    public string Name { get; } = Name;

    public Ward CreateWard(string? name)
    {
        // Blank ward name should default to the department name.
        var ward = new Ward(name ?? this.Name, this);
        this.wards.Add(ward);

        return ward;
    }
}
