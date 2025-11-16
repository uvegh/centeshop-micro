


using AutoMapper;
using Catalog.API.DTOs;
using Catalog.Domain.Entities;
using Catalog.Domain.Interfaces;
using Catalog.Infrastructure.Data;



namespace Catalog.Application.Features.Command;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly CatalogDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    private readonly IProductRepository _productRepo;
    public CreateProductCommandHandler( IProductRepository productRepo , ILogger<CreateProductCommandHandler> logger,IMapper mapper)
    {
        _productRepo = productRepo;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand req,CancellationToken ct)
    {
        var newProduct = ProductEntity.Create(req.Name, req.Price, req.StockQuantity);

         await _productRepo.AddAsync(newProduct,ct);
        
      
        
        _logger.LogInformation("add new Product {res}", newProduct);

        return _mapper.Map<ProductDto>(newProduct);

    }

   
}
