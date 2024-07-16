namespace TinMonkey.SimCare.Api.Configuration;

public class CosmosOptions
{
    public const string Cosmos = nameof(Cosmos);

    public string DatabaseName { get; set; } = string.Empty;

    public string EndpointUri { get; set; } = string.Empty;

    public string PrimaryKey { get; set; } = string.Empty;
}
