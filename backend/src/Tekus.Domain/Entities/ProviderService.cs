using Tekus.Domain.Common;

namespace Tekus.Domain.Entities;

public class ProviderService : Entity<Guid>
{
    public Guid ProviderId { get; private set; }
    public Guid ServiceId { get; private set; }
    public decimal? CustomHourlyRate { get; private set; }

    public Provider Provider { get; private set; } = null!;
    public Service Service { get; private set; } = null!;

    private ProviderService() { } // For EF Core

    private ProviderService(Guid providerId, Guid serviceId, decimal? customHourlyRate)
    {
        Id = Guid.NewGuid();
        ProviderId = providerId;
        ServiceId = serviceId;
        CustomHourlyRate = customHourlyRate;
    }

    public static ProviderService Create(Guid providerId, Guid serviceId, decimal? customHourlyRate = null)
    {
        return new ProviderService(providerId, serviceId, customHourlyRate);
    }
}
