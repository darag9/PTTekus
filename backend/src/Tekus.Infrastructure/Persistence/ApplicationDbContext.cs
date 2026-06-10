using MediatR;
using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Common;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ProviderService> ProviderServices => Set<ProviderService>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity<Guid>>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedAt = DateTime.UtcNow;
            }
        }

        var domainEvents = ChangeTracker.Entries<Entity<Guid>>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .SelectMany(x =>
            {
                var events = x.DomainEvents.ToList();
                x.ClearDomainEvents();
                return events;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
