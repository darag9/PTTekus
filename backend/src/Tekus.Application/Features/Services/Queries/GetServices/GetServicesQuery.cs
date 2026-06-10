using MediatR;
using Tekus.Application.Common;
using Tekus.Application.Features.Services.DTOs;

namespace Tekus.Application.Features.Services.Queries.GetServices;

public record GetServicesQuery(
    string? Search,
    string? SortBy,
    bool Ascending,
    int Page,
    int PageSize) : IRequest<PagedResult<ServiceDto>>;
