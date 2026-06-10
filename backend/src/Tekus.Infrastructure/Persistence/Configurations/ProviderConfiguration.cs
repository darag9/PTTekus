using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("Providers");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Nit).IsUnique();

        builder.Property(x => x.Nit).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.WebsiteUrl).HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Country).IsRequired().HasMaxLength(100);

        builder.HasMany(x => x.ProviderServices)
            .WithOne(x => x.Provider)
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Provider.ProviderServices))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
