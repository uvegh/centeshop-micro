


using Shared.Library.DTOs;

namespace Shared.Library.Events.Cart;


public class CartCheckedOut
{
    public Guid UserId { get; set; }
    public List<CartItemMessageDto> Items { get; set; } = new();
    public decimal Total { get; set; }
    public string Address { get;set; }
}
