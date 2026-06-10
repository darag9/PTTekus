using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Infrastructure.Persistence.Repositories;

public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    private readonly ApplicationDbContext _context;

    public ProviderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Provider?> GetByNitAsync(string nit, CancellationToken cancellationToken = default)
    {
        return await _context.Providers.FirstOrDefaultAsync(p => p.Nit == nit, cancellationToken);
    }

    public async Task<(IEnumerable<Provider> Items, int TotalCount)> GetPagedAsync(
        string? search, 
        string? sortBy, 
        bool ascending, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Providers.Include(p => p.ProviderServices).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.Nit.Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = sortBy?.ToLower() switch
        {
            "nit" => ascending ? query.OrderBy(p => p.Nit) : query.OrderByDescending(p => p.Nit),
            "name" => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "country" => ascending ? query.OrderBy(p => p.Country) : query.OrderByDescending(p => p.Country),
            _ => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name)
        };

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<Provider?> GetWithServicesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Providers
            .Include(p => p.ProviderServices)
            .ThenInclude(ps => ps.Service)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<(string Country, int Count)>> GetProviderCountByCountryAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.Providers
            .GroupBy(p => p.Country)
            .Select(g => new { Country = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return result.Select(x => (x.Country, x.Count));
    }
}
