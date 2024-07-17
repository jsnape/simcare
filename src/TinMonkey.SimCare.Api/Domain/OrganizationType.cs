namespace TinMonkey.SimCare.Api.Domain;

public class OrganizationType(Coding[] Coding, string? Text) : CodeableConcept(Coding, Text)
{
    private const string Version = "0.1.0";
    private static readonly Uri System = new("http://terminology.hl7.org/CodeSystem/organization-type");

    public static readonly OrganizationType Provider = new(
        [new Coding(System, Version, "prov", "Healthcare Provider", false)],
        "An organization that provides healthcare services.");

    public static readonly OrganizationType Department = new(
        [new Coding(System, Version, "dept", "Hospital Department", false)],
        "A department or ward within a hospital.");

    public static readonly OrganizationType Team = new(
        [new Coding(System, Version, "team", "Organizational Team", false)],
        "An organizational team is usually a grouping of practitioners that perform a specific function within an organization.");

    public static readonly OrganizationType Other = new(
        [new Coding(System, Version, "other", "Other", false)],
        "Other type of organization not already specified.");

    public OrganizationType()
        : this([], null)
    {
    }
}
