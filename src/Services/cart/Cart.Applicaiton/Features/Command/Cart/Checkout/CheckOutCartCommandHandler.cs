

using Cart.Domain.IRepository;
using Shared.Library.Events.Cart;
using MassTransit;
using Shared.Library.DTOs;

namespace Cart.Application.Features.Command.Cart.Checkout;

public class CheckOutCartCommandHandler:IRequestHandler<CheckOutCartCommand, CartCheckedOut>
{
    private readonly ICartRespository _cartRepo;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger _logger;

    public CheckOutCartCommandHandler(ILogger<CheckOutCartCommandHandler> logger,ICartRespository cartRespository, IPublishEndpoint publishEndpoint)
    {
        _cartRepo = cartRespository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;



    }

    public async Task<CartCheckedOut?>  Handle( CheckOutCartCommand req, CancellationToken ct)
    {
        var cart =await _cartRepo.GetByUserIdAsync(req.UserId);
        if (cart != null)
        {
           
            var items = cart.Items;
        decimal totalPrice=    items.Sum(x => x.UnitPrice * x.Quantity);
         

            var checkOutItems = items.Select(x => new CartItemMessageDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
                ,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice
            }).ToList();

            
         //var tp=   checkOutItems.Sum(x => x.Quantity * x.UnitPrice);
            _logger.LogInformation(" total price ={tp}", totalPrice);
            var @event = new CartCheckedOut
            {
                UserId =
                req.UserId,
                Items = checkOutItems,
                Total = totalPrice
            };

            //publish cartchecked out
          await  _publishEndpoint.Publish<CartCheckedOut>(@event, ct);
            return @event;

        }

        return null;
            
        }



    }


