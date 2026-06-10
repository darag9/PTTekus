using MediatR;

namespace Tekus.Application.Features.Providers.Commands.RemoveServiceFromProvider;

public record RemoveServiceFromProviderCommand(
    Guid ProviderId,
    Guid ServiceId) : IRequest;
