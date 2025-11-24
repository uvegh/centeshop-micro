

using Ordering.Domain.Interface;
using Shared.Library.Events.Cart;
namespace Ordering.API.Consumers;

public class CartCheckedOutConsumer:IConsumer<CartCheckedOut>
{
    private readonly IOrderingRepository _orderingRepo;
    private readonly ILogger _logger;
    public CartCheckedOutConsumer( IOrderingRepository orderingRepository,ILogger<CartCheckedOutConsumer> logger)
    {
        _logger = logger;
        _orderingRepo = orderingRepository;


    }

    public async Task Consume( ConsumeContext<CartCheckedOut> context)
    {
        //get cart items 
        var message = context.Message;
        _logger.LogInformation("Received  Cartcheckout for {UserId} total- {Count}",  message.UserId, message.Items.Count);
        var orderItems = message.Items.Select(i => new OrderItem(
            i.ProductId, i.Quantity, i.UnitPrice, i.ProductName
            )).ToList();
        var order = Order.Create(message.UserId, message.Total, orderItems, message.Address);
        await _orderingRepo.AddAsync(order, CancellationToken.None);
        _logger.LogInformation("Order created successfully => OrderId: {OrderId}", order.Id);
        //CartCheckedOut res= new(order.UserId, orderItems, order.TotalAmount, order.Address);
        //return new CartCheckedOut(message.UserId, orderItems, order.TotalAmount, order.Address);
        

    }

    
}
