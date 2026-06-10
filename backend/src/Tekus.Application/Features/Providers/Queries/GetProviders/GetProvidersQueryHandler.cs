using AutoMapper;
using MediatR;
using Tekus.Application.Common;
using Tekus.Application.Features.Providers.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Queries.GetProviders;

public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, PagedResult<ProviderDto>>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;

    public GetProvidersQueryHandler(IProviderRepository providerRepository, IMapper mapper)
    {
        _providerRepository = providerRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProviderDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _providerRepository.GetPagedAsync(
            request.Search,
            request.SortBy,
            request.Ascending,
            request.Page,
            request.PageSize,
            cancellationToken);

        var dtoItems = _mapper.Map<List<ProviderDto>>(items);

        return new PagedResult<ProviderDto>(dtoItems, totalCount, request.Page, request.PageSize);
    }
}
