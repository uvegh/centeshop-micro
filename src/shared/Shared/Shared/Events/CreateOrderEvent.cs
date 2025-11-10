

namespace Shared.Library.Events;

public  record CreateOrderEvent(
    Guid OrderId, Guid UserId, decimal TotalAmount
    ):BaseDomainEvent;

