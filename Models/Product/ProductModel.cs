namespace RavenShop.Application.Models.Product;

public class ProductModel
{
    public string? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Supplier { get; set; }
    [Required]
    public string? Category { get; set; }
    [Required]
    public string? QuantityPerUnit { get; set; }
    [Required]
    public decimal? PricePerUnit { get; set; }
    [Required]
    public decimal? UnitsInStock { get; set; }
    [Required]
    public decimal? UnitsOnOrder { get; set; }
    [Required]
    public bool? Discontinued { get; set; }
    [Required]
    public long? ReorderLevel { get; set; }
}

