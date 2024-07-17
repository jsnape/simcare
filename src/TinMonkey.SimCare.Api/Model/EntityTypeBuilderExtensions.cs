using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinMonkey.SimCare.Api.Domain;

namespace TinMonkey.SimCare.Api.Model;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureDomainResourceContainer<TEntity>(
        this EntityTypeBuilder<TEntity> builder, string containerName) where TEntity : DomainResource
    {
        builder
            .ToContainer(containerName)
            .HasKey(e => e.Uid);

        builder
            .HasPartitionKey(e => e.Uid)
            .Property(p => p.ETag)
            .IsETagConcurrency();

        builder.Property(p => p.Uid).IsRequired();

        builder.OwnsOne(p => p.Text, text =>
        {
            text
                .Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            text
                .Property(p => p.Div)
                .IsRequired();
        });

        return builder;
    }

}
