using Microsoft.AspNetCore.Mvc;
using Shared.Library.Model;
using Catalog.API.DTOs;
using AutoMapper;
using MediatR;
using Catalog.Application.Features.Command;


namespace Catalog.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;

        _mediator = mediator;

    }

    //in-memory



    //[HttpGet]

    //public async Task<ActionResult<List<Product>>> GetAll()
    //{
    //    return Ok(new ApiResponse<List<Product>>
    //    {
    //        Success = true,
    //        Data = _products,
    //        Message = "successfully retrieved"

    //    });

    //}

    //[HttpGet("{id}")]

    //public async Task<ActionResult<Product>> GetProduct ([FromRoute] Guid id)
    //{
    //    var product = _products.FirstOrDefault(x => (x.Id == id));

    //    if (id == null)
    //    {
    //        return NotFound( new ApiResponse<Product>
    //        {
    //            Success= false,
    //            Message="Not found"
    //        });
    //    }


    //    return Ok(new ApiResponse<Product>
    //    {
    //        Success = true,
    //        Data = _products[1],
    //        Message = "successfully retrieved"

    //    });
    //}

    [HttpPost]
                public async Task<ActionResult<ProductDto>> AddProduct(CreateProductCommand command)
    {
        await _mediator.Send(command);
        // You may want to return a result or status here, e.g.:
        // return Ok();
        // or return CreatedAtAction(...);
        return Ok(command);
    }
    }

