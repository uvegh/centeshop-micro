

using Shared.Library.Model;

namespace Ordering.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public List<OrderItem> Items {get;set;}
    public int Quaatity { get; set; }
}
