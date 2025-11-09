
using Microsoft.AspNetCore.Mvc;
using Shared.Library.Model;
using Catalog.API.DTOs;
using AutoMapper;


namespace Catalog.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;

    public ProductsController(IMapper mapper)
    {
       _mapper = mapper;

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

    //[HttpPost]

    //public async Task<ActionResult<ProductDto>> AddProduct(CreateProductDto product )
    //{
    //    var obj = _mapper.Map<Product>(product);
      
        

         
        
    //    var res = _mapper.Map<ProductDto>(obj);
    //    return CreatedAtAction(nameof(GetProduct), new {id=obj.Id},


    //        new ApiResponse<ProductDto>
    //        {
    //            Data = res,
    //            Message = "Product created",
    //            Success = true

    //        });
         
      



    //}


}
