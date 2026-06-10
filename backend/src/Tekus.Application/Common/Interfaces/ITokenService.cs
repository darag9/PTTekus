using Tekus.Domain.Entities;

namespace Tekus.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
