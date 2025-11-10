

using Microsoft.Extensions.Options;
using Ordering.Domain.Common;
using Ordering.Domain.Events;
using Shared.Library.Model;

namespace Ordering.Domain.Entities;

public class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public List<OrderItem> Items {get; private set;}
   
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = "Pending";

    public Order(Guid userId,List<OrderItem> items, decimal totalAmount, string status)
    {

        UserId = userId;
        Items = items;
        TotalAmount = totalAmount;
        Status = status;

        //automatically add new order domain event
        AddDomainEvent(new OrderCreatedDomainEvent(Id, UserId, TotalAmount));
    }
    public void MaskAsPaid()
    {
        Status = "Paid";
    }
}
