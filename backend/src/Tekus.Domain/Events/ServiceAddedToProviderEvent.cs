using Tekus.Domain.Common;

namespace Tekus.Domain.Events;

public class ServiceAddedToProviderEvent : IDomainEvent
{
    public Guid ProviderId { get; }
    public string ProviderName { get; }
    public Guid ServiceId { get; }
    public string ServiceName { get; }
    public DateTime OccurredOn { get; }

    public ServiceAddedToProviderEvent(Guid providerId, string providerName, Guid serviceId, string serviceName, DateTime occurredOn)
    {
        ProviderId = providerId;
        ProviderName = providerName;
        ServiceId = serviceId;
        ServiceName = serviceName;
        OccurredOn = occurredOn;
    }
}
