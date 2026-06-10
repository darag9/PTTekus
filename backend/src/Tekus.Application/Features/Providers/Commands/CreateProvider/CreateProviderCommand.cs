using MediatR;

namespace Tekus.Application.Features.Providers.Commands.CreateProvider;

public record CreateProviderCommand(
    string Nit,
    string Name,
    string WebsiteUrl,
    string Email,
    string Country) : IRequest<Guid>;
