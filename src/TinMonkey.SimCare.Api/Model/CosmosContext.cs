using Microsoft.EntityFrameworkCore;
using TinMonkey.SimCare.Api.Domain;

namespace TinMonkey.SimCare.Api.Model;

public class CosmosContext(DbContextOptions<CosmosContext> Options) : DbContext(Options)
{
    public DbSet<Encounter> Encounters { get; set; }

    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CosmosContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
