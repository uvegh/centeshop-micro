using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Entities;
namespace Ordering.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        public  static readonly List<OrderItem> _orders = new();
        private readonly IMapper _mapper;
        public OrderController(IMapper mapper)
        {
            _mapper = mapper;

     
        }

        //[HttpPost]

        //public async Task <ActionResult<Order>>
     

    }
}
