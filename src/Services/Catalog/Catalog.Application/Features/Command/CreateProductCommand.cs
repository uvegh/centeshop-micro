using MediatR;

namespace Catalog.Application.Features.Command;

public  record  CreateProductCommand( string Name, decimal Price, int StockQuantity) :IRequest;
