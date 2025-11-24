

using Shared.Library.Events.Cart;

namespace Cart.Application.Features.Command.Cart.Checkout;

public  record CheckOutCartCommand(Guid UserId, string Address) :IRequest<CartCheckedOut?>;

