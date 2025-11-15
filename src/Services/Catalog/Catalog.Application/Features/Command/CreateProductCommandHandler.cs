

using ProductEntity = Catalog.Domain.Entities.Product;
using Catalog.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Catalog.Application.Features.Command;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand>
{
    private readonly CatalogDbContext _dbContext;
    private readonly ILogger _logger;
    public CreateProductCommandHandler(CatalogDbContext dbContext, ILogger<CreateProductCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(CreateProductCommand req, CancellationToken ct)
    {
        var newProduct = ProductEntity.Create(req.Name, req.Price, req.StockQuantity);

         _dbContext. Add(newProduct);
      
        await _dbContext.SaveChangesAsync(ct);
        _logger.LogInformation("add new Product {newProduct}", newProduct);
    


    }
}
