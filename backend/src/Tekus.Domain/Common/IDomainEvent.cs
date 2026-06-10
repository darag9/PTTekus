using MediatR;

namespace Tekus.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
