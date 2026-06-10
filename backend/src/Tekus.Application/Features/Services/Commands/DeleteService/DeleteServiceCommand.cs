using MediatR;

namespace Tekus.Application.Features.Services.Commands.DeleteService;

public record DeleteServiceCommand(Guid Id) : IRequest;
