using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinMonkey.SimCare.Api.Domain;

namespace TinMonkey.SimCare.Api.Model;

public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .ConfigureDomainResourceContainer("Organizations");

        //builder.OwnsMany(e => e.Identifiers)
    }
}
