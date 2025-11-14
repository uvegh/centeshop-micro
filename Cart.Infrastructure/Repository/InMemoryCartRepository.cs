

using Cart.Domain.IRepository;
using CartEntity = Cart.Domain.Entities.Cart;
namespace Cart.Infrastructure.Repository;

public  class InMemoryCartRepository:ICartRespository
{
    //temporary in memorydb for carts
    private readonly Dictionary<Guid, CartEntity> _carts = new();
   
public Task<CartEntity?> GetByUserIdAsync(Guid UserId)
    {
        _carts.TryGetValue(UserId, out var cart);
        if (cart != null){
            Task.FromResult(cart);
        }
        return null;
       
      
    }

    public Task SaveAsync(CartEntity cart)
    {

        
        _carts[cart.UserId] = cart;
        return Task.CompletedTask;
    }
}
