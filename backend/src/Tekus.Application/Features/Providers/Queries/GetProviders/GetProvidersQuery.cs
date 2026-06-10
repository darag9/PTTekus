using MediatR;
using Tekus.Application.Common;
using Tekus.Application.Features.Providers.DTOs;

namespace Tekus.Application.Features.Providers.Queries.GetProviders;

public record GetProvidersQuery(
    string? Search,
    string? SortBy,
    bool Ascending,
    int Page,
    int PageSize) : IRequest<PagedResult<ProviderDto>>;
