namespace Tekus.Application.Features.Providers.DTOs;

public class ProviderDto
{
    public Guid Id { get; set; }
    public string Nit { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int ServiceCount { get; set; }
}

public class ProviderDetailDto : ProviderDto
{
    public List<ProviderServiceDto> Services { get; set; } = new();
}

public class ProviderServiceDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public decimal? CustomHourlyRate { get; set; }
}
