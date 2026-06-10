using MediatR;

namespace Tekus.Application.Features.Services.Commands.UpdateService;

public record UpdateServiceCommand(
    Guid Id,
    string Name,
    decimal HourlyRate) : IRequest;
