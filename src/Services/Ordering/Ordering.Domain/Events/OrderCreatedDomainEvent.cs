

using Shared.Library.Events;

namespace Ordering.Domain.Events;

public record  OrderCreatedDomainEvent(Guid OrderId,
    Guid UserId,
    decimal TotalAmount
    ):BaseDomainEvent;

