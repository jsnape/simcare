using System.Diagnostics;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name}")]
public class Bed(string Name)
{
    public string Name { get; } = Name;

    public Patient? CurrentPatient { get; set; }
}
