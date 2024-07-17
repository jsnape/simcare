using Newtonsoft.Json;

namespace TinMonkey.SimCare.Api.Domain;

public class DomainResource : Resource
{
    public Narrative? Text { get; set; }

    public string? ETag { get; set; }
}
