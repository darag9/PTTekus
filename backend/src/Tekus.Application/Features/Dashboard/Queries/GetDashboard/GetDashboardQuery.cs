using MediatR;
using Tekus.Application.Features.Dashboard.DTOs;

namespace Tekus.Application.Features.Dashboard.Queries.GetDashboard;

public record GetDashboardQuery : IRequest<DashboardDto>;
