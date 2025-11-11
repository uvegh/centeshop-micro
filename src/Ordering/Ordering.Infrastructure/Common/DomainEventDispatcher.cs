using MediatR;
using Ordering.Domain.Common;

namespace Ordering.Infrastructure.Common;

public class DomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventAsync(IEnumerable<AggregateRoot> aggregrates)
    {
        var domainEvents = aggregrates.SelectMany(x => x.DomainEvents).ToList();


        //publish each event through mediatr
        foreach( var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        foreach(var  aggregate in aggregrates)
        {
            aggregate.ClearDomainvents();
        }
    }
}
