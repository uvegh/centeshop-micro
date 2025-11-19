using Cart.Application.Features.Cart.Command.AddItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController:ControllerBase
{
    private readonly IMediator _mediator;
    public CartController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpPost("add")]

    public async Task<IActionResult> Add(AddToCartCommand req)
    {
          await _mediator.Send(new AddToCartCommand(req.UserId, req.ProductId, req.Quantity));

        return Ok("Item added");
    }

}
