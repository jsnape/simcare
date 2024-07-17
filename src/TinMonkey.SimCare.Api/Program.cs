using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using TinMonkey.SimCare.Api.Configuration;
using TinMonkey.SimCare.Api.Domain;
using TinMonkey.SimCare.Api.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Both of these string enum converters are required for the JSON serialization to work correctly.
// See: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2293
builder.Services.ConfigureHttpJsonOptions(
    options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.Configure<JsonOptions>(
    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddCosmos(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/encounters", (CosmosContext context) => context.Encounters)
.WithName("GetEncounters")
.WithOpenApi();

app.MapPost("/encounters", async (CosmosContext context, string patientName) =>
{
    var encounter = new Encounter
    {
        PatientName = patientName,
        Text = new Narrative(
            NarrativeStatus.Generated,
            "<div xmlns=\"http://www.w3.org/1999/xhtml\">Generated</div>"),
    };

    context.Encounters.Add(encounter);

    await context
        .SaveChangesAsync()
        .ConfigureAwait(false);

    return encounter;
})
.WithName("PostEncounter")
.WithOpenApi();

app.MapPost("/organizations", async (CosmosContext context) =>
{
    var org = new Organization
    {
        Name = "Acme Healthcare",
        Type = OrganizationType.Provider,
        Description = new Markdown("Acme Healthcare is a large healthcare provider."),
    };

    ////org.Identifiers.Add(new Identifier(
    ////    IdentifierUse.Official, IdentifierType.ProviderNumber, null, "/acme"));    

    org.Aliases.Add("acme");

    context.Organizations.Add(org);

    await context
        .SaveChangesAsync()
        .ConfigureAwait(false);

    return org;
})
.WithName("PostOrganization")
.WithOpenApi();

var cosmos = app.Services.GetRequiredService<CosmosClient>();
var cosmosOptions = app.Configuration.GetSection(CosmosOptions.Cosmos).Get<CosmosOptions>();

await cosmos
    .CreateDatabaseIfNotExistsAsync(cosmosOptions!.DatabaseName)
    .ConfigureAwait(false);

var database = cosmos.GetDatabase(cosmosOptions.DatabaseName);

var containers = new[]
{
    new ContainerProperties("Encounters", "/Uid"),
    new ContainerProperties("Organizations", "/Uid"),
};

foreach (var container in containers)
{
    await database
        .CreateContainerIfNotExistsAsync(container)
        .ConfigureAwait(false);
}

app.Run();
