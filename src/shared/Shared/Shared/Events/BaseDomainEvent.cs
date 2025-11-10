


namespace Shared.Library.Events;

public abstract record BaseDomainEvent : IDomainEvent
{
  
  //public  Guid EventId { get; set; }
  public  DateTime OccuredAt { get; init; } = DateTime.UtcNow;


}
