using MediatR;
using Tekus.Application.Features.Providers.DTOs;

namespace Tekus.Application.Features.Providers.Queries.GetProviderById;

public record GetProviderByIdQuery(Guid Id) : IRequest<ProviderDetailDto>;
