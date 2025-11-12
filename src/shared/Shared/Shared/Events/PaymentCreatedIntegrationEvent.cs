

namespace Shared.Library.Events;

public record  PaymentCreatedIntegrationEvent(Guid OrderId, Guid UserId, decimal TotalAmount);
