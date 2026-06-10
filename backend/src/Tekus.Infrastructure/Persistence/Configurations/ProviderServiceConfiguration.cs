using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Configurations;

public class ProviderServiceConfiguration : IEntityTypeConfiguration<ProviderService>
{
    public void Configure(EntityTypeBuilder<ProviderService> builder)
    {
        builder.ToTable("ProviderServices");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.ProviderId, x.ServiceId }).IsUnique();

        builder.Property(x => x.CustomHourlyRate).HasColumnType("decimal(18,2)");
    }
}
