using Tekus.Domain.Entities;
using Tekus.Domain.Events;

namespace Tekus.Domain.Tests;

public class ProviderTests
{
    [Fact]
    public void CreateProvider_WithValidData_ReturnsProvider()
    {
        // Act
        var provider = Provider.Create("12345", "Test Provider", "http://test.com", "test@test.com", "USA");

        // Assert
        Assert.NotNull(provider);
        Assert.Equal("12345", provider.Nit);
        Assert.Equal("Test Provider", provider.Name);
        Assert.Equal("USA", provider.Country);
    }

    [Fact]
    public void AddService_WithValidService_AddsToProviderServicesAndRaisesEvent()
    {
        // Arrange
        var provider = Provider.Create("12345", "Test Provider", "http://test.com", "test@test.com", "USA");
        var serviceId = Guid.NewGuid();

        // Act
        provider.AddService(serviceId, "Test Service");

        // Assert
        Assert.Single(provider.ProviderServices);
        Assert.Equal(serviceId, provider.ProviderServices.First().ServiceId);
        
        var domainEvent = Assert.Single(provider.DomainEvents);
        Assert.IsType<ServiceAddedToProviderEvent>(domainEvent);
    }
}
