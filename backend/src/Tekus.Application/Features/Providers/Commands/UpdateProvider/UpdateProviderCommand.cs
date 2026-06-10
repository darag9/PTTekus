using MediatR;

namespace Tekus.Application.Features.Providers.Commands.UpdateProvider;

public record UpdateProviderCommand(
    Guid Id,
    string Nit,
    string Name,
    string WebsiteUrl,
    string Email,
    string Country) : IRequest;
