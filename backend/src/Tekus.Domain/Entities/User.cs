using Tekus.Domain.Common;

namespace Tekus.Domain.Entities;

public class User : Entity<Guid>
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;

    private User() { } // For EF Core

    private User(string email, string passwordHash, string fullName, string role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = role;
    }

    public static User Create(string email, string passwordHash, string fullName, string role = "Admin")
    {
        return new User(email, passwordHash, fullName, role);
    }
}
