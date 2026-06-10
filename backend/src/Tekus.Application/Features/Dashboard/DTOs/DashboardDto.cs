namespace Tekus.Application.Features.Dashboard.DTOs;

public class CountryCountDto
{
    public string Country { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class DashboardDto
{
    public List<CountryCountDto> ProvidersByCountry { get; set; } = new();
    public List<CountryCountDto> ServicesByCountry { get; set; } = new();
    public int TotalProviders { get; set; }
    public int TotalServices { get; set; }
    public int TotalCountries { get; set; }
}
