using MediatR;
using Tekus.Application.Features.Services.DTOs;

namespace Tekus.Application.Features.Services.Queries.GetServiceById;

public record GetServiceByIdQuery(Guid Id) : IRequest<ServiceDto>;
