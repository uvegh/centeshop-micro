using Cart.Domain.Entities;

namespace Cart.Application.Features.Command.Cart.UpdateItem;

public record  UpdateCartItemQuantityCommand(Guid UserId, Guid ProductId, int Quantity):IRequest<bool>;

