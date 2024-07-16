using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinMonkey.SimCare.Medicine.Fhir;

namespace TinMonkey.SimCare.Api.Model;

public class EncounterEntityTypeConfiguration : IEntityTypeConfiguration<Encounter>
{
    public void Configure(EntityTypeBuilder<Encounter> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .ToContainer("Encounters")
            .HasPartitionKey(e => e.Id)
            .UseETagConcurrency();
    }
}
