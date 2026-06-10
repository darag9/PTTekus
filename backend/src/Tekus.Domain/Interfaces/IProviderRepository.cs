using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Domain.Interfaces;

public interface IProviderRepository : IRepository<Provider>
{
    Task<Provider?> GetByNitAsync(string nit, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Provider> Items, int TotalCount)> GetPagedAsync(string? search, string? sortBy, bool ascending, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Provider?> GetWithServicesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<(string Country, int Count)>> GetProviderCountByCountryAsync(CancellationToken cancellationToken = default);
}
