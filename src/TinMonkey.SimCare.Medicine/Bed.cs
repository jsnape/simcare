using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name} Ward {Ward}")]
public class Bed(string Name, Ward Ward) : ILocation
{
    public string Name { get; } = Name;

    public Ward Ward { get; } = Ward;
}
