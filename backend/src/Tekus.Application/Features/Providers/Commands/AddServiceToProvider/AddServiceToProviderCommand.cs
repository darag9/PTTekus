using MediatR;

namespace Tekus.Application.Features.Providers.Commands.AddServiceToProvider;

public record AddServiceToProviderCommand(
    Guid ProviderId,
    Guid ServiceId,
    decimal? CustomHourlyRate = null) : IRequest;
