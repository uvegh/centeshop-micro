using Cart.Domain.Entities;
using Cart.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Features.Command.Cart.UpdateItem
{
    public class UpdateCartItemQuantityCommandHandler:IRequestHandler<UpdateCartItemQuantityCommand,CartEntity>
    {
        private readonly ILogger _logger;
        private readonly ICartRespository _cartRepo; 
        public UpdateCartItemQuantityCommandHandler(ILogger<UpdateCartItemQuantityCommandHandler> logger, ICartRespository cartRepo)
        {
            _logger = logger;
            _cartRepo = cartRepo;
            
        }

        public async Task<CartItem> Handle(UpdateCartItemQuantityCommand req ,CancellationToken ct)
        {
            var cart = await _cartRepo.GetByUserIdAsync(req.UserId);
            if (cart != null)
            {
            var item=    cart.Items.FirstOrDefault(x => x.ProductId == req.ProductId);

                if (item != null)
                {
                    item.UpdateQuantity(req.Quantity);

                    await _cartRepo.
                    return item;

                }
                
            }
            


        }
    }
}
