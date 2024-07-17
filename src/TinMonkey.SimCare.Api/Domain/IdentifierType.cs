namespace TinMonkey.SimCare.Api.Domain;

public class IdentifierType(Coding[] Coding, string? Text) : CodeableConcept(Coding, Text)
{
    private const string Version = "5.0.0";
    private static readonly Uri System = new("http://hl7.org/fhir/ValueSet/identifier-type");

    public static readonly IdentifierType ProviderNumber = new(
        [new Coding(System, Version, "prn", "Provider Number", false)],
        "A number that is unique to an individual provider, a provider group or an organization within an Assigning Authority.");

    public IdentifierType()
        : this([], null)
    {
    }
}
