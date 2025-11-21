



namespace Cart.Application.DTOs;

public class CartDto { 
    public  Guid UserId { get; set; }
        
     public    List<CartItemDto>? Items { get; set; }
        
        }

public class CheckOutDto
{
    public Guid UserId { get; set; }

    public List<CartItemDto>? Items { get; set; }
    public decimal Total { get; set; }

};

public class UpdateQuantityDto
{

    public int Quantity { get; set; }
}


