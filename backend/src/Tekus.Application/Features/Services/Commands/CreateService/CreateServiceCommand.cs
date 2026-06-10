using MediatR;

namespace Tekus.Application.Features.Services.Commands.CreateService;

public record CreateServiceCommand(
    string Name,
    decimal HourlyRate) : IRequest<Guid>;
