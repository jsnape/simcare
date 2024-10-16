using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinMonkey.SimCare.Api.Domain;

namespace TinMonkey.SimCare.Api.Model;

public class EncounterEntityTypeConfiguration : IEntityTypeConfiguration<Encounter>
{
    public void Configure(EntityTypeBuilder<Encounter> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .ConfigureDomainResourceContainer("Organizations");

        builder.Property(p => p.PatientName).IsRequired();
    }
}
