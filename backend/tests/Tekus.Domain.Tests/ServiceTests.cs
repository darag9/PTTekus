using Tekus.Domain.Entities;

namespace Tekus.Domain.Tests;

public class ServiceTests
{
    [Fact]
    public void CreateService_WithValidData_ReturnsService()
    {
        // Act
        var service = Service.Create("Test Service", 50.0m);

        // Assert
        Assert.NotNull(service);
        Assert.Equal("Test Service", service.Name);
        Assert.Equal(50.0m, service.HourlyRate);
    }

    [Fact]
    public void UpdateService_WithValidData_UpdatesProperties()
    {
        // Arrange
        var service = Service.Create("Test Service", 50.0m);

        // Act
        service.Update("Updated Service", 60.0m);

        // Assert
        Assert.Equal("Updated Service", service.Name);
        Assert.Equal(60.0m, service.HourlyRate);
    }
}
