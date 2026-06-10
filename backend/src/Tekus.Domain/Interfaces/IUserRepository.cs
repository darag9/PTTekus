using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
