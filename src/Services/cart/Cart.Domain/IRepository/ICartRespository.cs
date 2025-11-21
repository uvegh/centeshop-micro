using Cart.Domain.Entities;

using   CartEntity = Cart.Domain.Entities.Cart;
namespace Cart.Domain.IRepository;

public interface ICartRespository
{
    Task<CartEntity> GetByUserIdAsync(Guid UserId);
    //Task SaveCartAsync(CartEntity cart);
    Task AddCartItemAsync(Guid userId, CartItem item);
    Task RemoveCartItemAsync(Guid userId, Guid productId);
    Task UpdateCartItemAsync(Guid UserId, Guid ProductId, CartItem cartItem);
}
