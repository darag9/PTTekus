using MediatR;

namespace Tekus.Application.Features.Providers.Commands.DeleteProvider;

public record DeleteProviderCommand(Guid Id) : IRequest;
