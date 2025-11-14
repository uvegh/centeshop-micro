
using Ordering.Application.Features.Order.Command;
using OrderEntity=Ordering.Domain.Entities.Order;
using Ordering.Infrastructure.Data;



namespace Ordering.Application.Features.Order.Command;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly OrderingDbContext _context;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    public CreateOrderCommandHandler(OrderingDbContext context, ILogger<CreateOrderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle( CreateOrderCommand req ,CancellationToken ct)
    {
        // call the create function  added in the order entity
       
        var order = OrderEntity.Create(
     req.userId, req.totalAmount);

        _context.Add(order);
        await _context.SaveChangesAsync(ct);// calls the overriden method created to add all domain and aggregates

        _logger.LogInformation("order created, new id -{Id}", order.Id);


        return order.Id;
    }

  
}
