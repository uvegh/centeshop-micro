
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Order.Command;
using Ordering.Application.Features.Order.Query;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        
     
        private readonly IMediator _mediator;
        public OrderController( IMediator mediator)
        {
            _mediator = mediator; 

     
        }

        [HttpPost]

       public async Task<ActionResult> Create(CreateOrderCommand command)
        {
        var res=    await _mediator.Send(command);
            return Ok(res);

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var res = await _mediator.Send(new GetOrdersQuery());
                return Ok( res);
        }



       
        

    }
}
