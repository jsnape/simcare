namespace TinMonkey.SimCare.Api.Domain;

public class Organization : DomainResource
{
    public IList<Identifier> Identifiers { get; } = [];

    public bool Active { get; set; } = true;

    public OrganizationType? Type { get; set; }

    public string? Name { get; set; }

    public ISet<string> Aliases { get; } = new HashSet<string>();

    public Markdown? Description { get; set; }

    ////public Reference? PartOf { get; set; }
}
