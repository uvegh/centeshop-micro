using MediatR;

namespace Ordering.Infrastructure.Common;

public class DomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventAsync()
    {
        

    }
}
