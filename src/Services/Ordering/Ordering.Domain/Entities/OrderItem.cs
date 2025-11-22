

namespace Ordering.Domain.Entities;

public  class OrderItem
{
    public Guid Id { get; private set; } = new Guid();
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get;private set; }
    public OrderItem(Guid productId, int quantity, decimal unitPrice, string productName)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ProductName = productName;

    }
    private OrderItem()
    {
    }
}


