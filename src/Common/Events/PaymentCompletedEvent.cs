

namespace Shared.Library.Events;

public  record PaymentCompletedEvent(
    Guid OrderId,
    bool Sucess
    );
