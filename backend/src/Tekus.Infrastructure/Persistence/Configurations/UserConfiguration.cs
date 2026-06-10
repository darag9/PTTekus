using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(50);
    }
}
