


using Ordering.Domain.Common;
using Ordering.Domain.Events;


namespace Ordering.Domain.Entities;

public  class Order : AggregateRoot
{

    private Order()
    {

    }
    public Guid UserId { get; private set; }
    private readonly List<OrderItem> _items  =new ();
    public IReadOnlyCollection<OrderItem> Items  =>_items.AsReadOnly();
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = "Pending";

    public string Address { get; private set; }
    public Order(Guid userId,List<OrderItem> items, decimal totalAmount, string status,string address)
    {

        UserId = userId;
        _items = items ?? new List<OrderItem>();

        TotalAmount = totalAmount;
        Status = status;
        Address = address;
       
    }

    public static Order Create( Guid userId, decimal totalAmount, List<OrderItem> items,string address )
    {

        if(items==null || items.Count == 0)
        {
            throw  new Exception("order must have at least one item");
        }
        var totAmount = items.Sum(x => x.Quantity * x.UnitPrice);
        var order = new Order(userId, items, totAmount, "Pending",address);
        //automatically add new order domain event

     order.AddDomainEvent(new OrderCreatedDomainEvent(order.Id, userId, totAmount));
        
        return order;
    }
    public void MaskAsPaid()
    {
        Status = "Paid";
    }
}
