namespace TinMonkey.SimCare.Medicine.Fhir;

public class DomainResource : Resource
{
    public Narrative? Text { get; set; }

    public IList<Resource> Contained { get; } = [];
}
