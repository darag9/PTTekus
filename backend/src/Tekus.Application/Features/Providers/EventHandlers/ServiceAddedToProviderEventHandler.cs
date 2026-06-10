using MediatR;
using Tekus.Application.Common.Interfaces;
using Tekus.Domain.Events;

namespace Tekus.Application.Features.Providers.EventHandlers;

public class ServiceAddedToProviderEventHandler : INotificationHandler<ServiceAddedToProviderEvent>
{
    private readonly IEmailService _emailService;

    public ServiceAddedToProviderEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(ServiceAddedToProviderEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendServiceAddedNotificationAsync(
            notification.ProviderName, 
            notification.ServiceName, 
            cancellationToken);
    }
}
