using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tekus.Application.Common.Interfaces;

namespace Tekus.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;

    public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public Task SendServiceAddedNotificationAsync(string providerName, string serviceName, CancellationToken cancellationToken = default)
    {
        var toEmail = _configuration["SystemPreferences:NotificationEmail"] ?? "admin@tekus.com";
        
        // In a real application, you would use SmtpClient or a service like SendGrid here.
        // For development/demonstration, we will just log it.
        _logger.LogInformation(
            "EMAIL SENT to {ToEmail}: Provider '{ProviderName}' has enabled a new service '{ServiceName}'.", 
            toEmail, providerName, serviceName);

        return Task.CompletedTask;
    }
}
