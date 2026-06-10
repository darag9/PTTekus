namespace Tekus.Domain.Common;

public abstract class AuditableEntity<TId> : Entity<TId> where TId : notnull
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
