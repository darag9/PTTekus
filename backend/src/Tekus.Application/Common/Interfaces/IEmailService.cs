namespace Tekus.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendServiceAddedNotificationAsync(string providerName, string serviceName, CancellationToken cancellationToken = default);
}
