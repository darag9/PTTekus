using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Infrastructure.Persistence.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Service> Items, int TotalCount)> GetPagedAsync(
        string? search, 
        string? sortBy, 
        bool ascending, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Services.Include(s => s.ProviderServices).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(s => s.Name.Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = sortBy?.ToLower() switch
        {
            "name" => ascending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
            "hourlyrate" => ascending ? query.OrderBy(s => s.HourlyRate) : query.OrderByDescending(s => s.HourlyRate),
            _ => ascending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name)
        };

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IEnumerable<(string Country, int Count)>> GetServiceCountByCountryAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.ProviderServices
            .Include(ps => ps.Provider)
            .GroupBy(ps => ps.Provider.Country)
            .Select(g => new { Country = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return result.Select(x => (x.Country, x.Count));
    }
}
