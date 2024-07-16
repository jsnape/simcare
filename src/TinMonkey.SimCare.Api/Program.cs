using Microsoft.Azure.Cosmos;
using TinMonkey.SimCare.Api.Configuration;
using TinMonkey.SimCare.Api.Model;
using TinMonkey.SimCare.Medicine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

var cosmos = app.Services.GetRequiredService<CosmosClient>();
var cosmosOptions = app.Configuration.GetSection(CosmosOptions.Cosmos).Get<CosmosOptions>();

await cosmos
    .CreateDatabaseIfNotExistsAsync(cosmosOptions!.DatabaseName)
    .ConfigureAwait(false);

var database = cosmos.GetDatabase(cosmosOptions.DatabaseName);

var containers = new[]
{
    new ContainerProperties("encounters", "/Id"),
};

foreach (var container in containers)
{
    await database
        .CreateContainerIfNotExistsAsync(container)
        .ConfigureAwait(false);
}

app.Run();
