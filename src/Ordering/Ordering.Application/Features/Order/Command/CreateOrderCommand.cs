
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Order.Command;

public record CreateOrderCommand( Guid
     userId, List<OrderItem> items, decimal totalAmount ):IRequest<Guid>;

