using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Domain.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<(IEnumerable<Service> Items, int TotalCount)> GetPagedAsync(string? search, string? sortBy, bool ascending, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<(string Country, int Count)>> GetServiceCountByCountryAsync(CancellationToken cancellationToken = default);
}
