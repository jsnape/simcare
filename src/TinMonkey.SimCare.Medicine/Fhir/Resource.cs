using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine.Fhir;

public class Resource
{
    public Id Id { get; set; } = Guid.NewGuid().ToBase36();
}
