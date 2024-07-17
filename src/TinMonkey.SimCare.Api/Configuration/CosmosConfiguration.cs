using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TinMonkey.SimCare.Api.Model;

namespace TinMonkey.SimCare.Api.Configuration;

public static class CosmosConfiguration
{
    public static IServiceCollection AddCosmos(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<CosmosOptions>(configuration.GetSection(CosmosOptions.Cosmos));

        services.AddSingleton<CosmosClient>(serviceProvider =>
        {
            var options = serviceProvider
                .GetRequiredService<IOptions<CosmosOptions>>()
                .Value;

            var clientOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    IgnoreNullValues = true,
                }
            };

            return new CosmosClient(options.EndpointUri, options.PrimaryKey, clientOptions);
        });

        services.AddDbContext<CosmosContext>(options =>
        {
            var cosmosOptions = configuration
                .GetSection(CosmosOptions.Cosmos)
                .Get<CosmosOptions>();

            ArgumentNullException.ThrowIfNull(cosmosOptions);

            options.UseCosmos(
                cosmosOptions.EndpointUri,
                cosmosOptions.PrimaryKey,
                cosmosOptions.DatabaseName);
        });

        return services;
    }
}
