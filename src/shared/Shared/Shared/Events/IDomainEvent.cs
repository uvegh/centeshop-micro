

namespace Shared.Library.Events;

public interface IDomainEvent
{
    //Guid EventId { get; set; }
    DateTime OccuredAt { get;  }
}
