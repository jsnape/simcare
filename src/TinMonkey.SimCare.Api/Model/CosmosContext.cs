using Microsoft.EntityFrameworkCore;
using TinMonkey.SimCare.Medicine.Fhir;

namespace TinMonkey.SimCare.Api.Model;

public class CosmosContext(DbContextOptions Options) : DbContext(Options)
{
    public DbSet<Encounter> Encounters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CosmosContext).Assembly);
    }
}
