using AutoMapper;
using MediatR;
using Tekus.Application.Common;
using Tekus.Application.Features.Services.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, PagedResult<ServiceDto>>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public GetServicesQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _serviceRepository.GetPagedAsync(
            request.Search,
            request.SortBy,
            request.Ascending,
            request.Page,
            request.PageSize,
            cancellationToken);

        var dtoItems = _mapper.Map<List<ServiceDto>>(items);

        return new PagedResult<ServiceDto>(dtoItems, totalCount, request.Page, request.PageSize);
    }
}
