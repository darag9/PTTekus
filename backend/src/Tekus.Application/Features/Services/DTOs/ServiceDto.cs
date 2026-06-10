namespace Tekus.Application.Features.Services.DTOs;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProviderCount { get; set; }
}
