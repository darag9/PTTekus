using MediatR;
using Tekus.Application.Features.Dashboard.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Dashboard.Queries.GetDashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IServiceRepository _serviceRepository;

    public GetDashboardQueryHandler(IProviderRepository providerRepository, IServiceRepository serviceRepository)
    {
        _providerRepository = providerRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var providersByCountry = await _providerRepository.GetProviderCountByCountryAsync(cancellationToken);
        var servicesByCountry = await _serviceRepository.GetServiceCountByCountryAsync(cancellationToken);

        var providerCountryDtos = providersByCountry.Select(x => new CountryCountDto { Country = x.Country, Count = x.Count }).ToList();
        var serviceCountryDtos = servicesByCountry.Select(x => new CountryCountDto { Country = x.Country, Count = x.Count }).ToList();

        var totalProviders = providerCountryDtos.Sum(x => x.Count);
        var totalServices = await _serviceRepository.GetPagedAsync(null, null, true, 1, 1, cancellationToken);
        var totalCountries = providersByCountry.Select(x => x.Country).Distinct().Count();

        return new DashboardDto
        {
            ProvidersByCountry = providerCountryDtos,
            ServicesByCountry = serviceCountryDtos,
            TotalProviders = totalProviders,
            TotalServices = totalServices.TotalCount,
            TotalCountries = totalCountries
        };
    }
}
