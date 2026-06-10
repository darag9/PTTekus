using Tekus.Domain.Common;
using Tekus.Domain.Events;

namespace Tekus.Domain.Entities;

public class Provider : AuditableEntity<Guid>
{
    private readonly List<ProviderService> _providerServices = new();

    public string Nit { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? WebsiteUrl { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public IReadOnlyCollection<ProviderService> ProviderServices => _providerServices.AsReadOnly();

    private Provider() { } // For EF Core

    private Provider(string nit, string name, string? websiteUrl, string email, string country)
    {
        Id = Guid.NewGuid();
        Nit = nit;
        Name = name;
        WebsiteUrl = websiteUrl;
        Email = email;
        Country = country;
    }

    public static Provider Create(string nit, string name, string websiteUrl, string email, string country)
    {
        // Validation could be added here or handled by the command validator
        return new Provider(nit, name, websiteUrl, email, country);
    }

    public void Update(string name, string websiteUrl, string email, string country)
    {
        Name = name;
        WebsiteUrl = websiteUrl;
        Email = email;
        Country = country;
    }

    public void AddService(Guid serviceId, string serviceName, decimal? customHourlyRate = null)
    {
        if (_providerServices.Any(ps => ps.ServiceId == serviceId))
            return;

        var providerService = ProviderService.Create(Id, serviceId, customHourlyRate);
        _providerServices.Add(providerService);

        AddDomainEvent(new ServiceAddedToProviderEvent(Id, Name, serviceId, serviceName, DateTime.UtcNow));
    }

    public void RemoveService(Guid serviceId)
    {
        var service = _providerServices.FirstOrDefault(ps => ps.ServiceId == serviceId);
        if (service != null)
        {
            _providerServices.Remove(service);
        }
    }
}
