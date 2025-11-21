namespace Cart.Application.DTOs;

public class ProductDto
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

