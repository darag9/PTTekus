using Tekus.Domain.Common;

namespace Tekus.Domain.Entities;

public class Service : AuditableEntity<Guid>
{
    private readonly List<ProviderService> _providerServices = new();

    public string Name { get; private set; } = string.Empty;
    public decimal HourlyRate { get; private set; }

    public IReadOnlyCollection<ProviderService> ProviderServices => _providerServices.AsReadOnly();

    private Service() { } // For EF Core

    private Service(string name, decimal hourlyRate)
    {
        Id = Guid.NewGuid();
        Name = name;
        HourlyRate = hourlyRate;
    }

    public static Service Create(string name, decimal hourlyRate)
    {
        return new Service(name, hourlyRate);
    }

    public void Update(string name, decimal hourlyRate)
    {
        Name = name;
        HourlyRate = hourlyRate;
    }
}
