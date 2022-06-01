namespace RavenShop.Application.Models.Product;

public class Product
{
    [JsonProperty("Id")]
    public string? Id { get; set; }
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("Supplier")]
    public string? Supplier { get; set; }

    [JsonProperty("Category")]
    public string? Category { get; set; }

    [JsonProperty("QuantityPerUnit")]
    public string? QuantityPerUnit { get; set; }

    [JsonProperty("PricePerUnit")]
    public decimal? PricePerUnit { get; set; }

    [JsonProperty("UnitsInStock")]
    public decimal? UnitsInStock { get; set; }

    [JsonProperty("UnitsOnOrder")]
    public decimal? UnitsOnOrder { get; set; }

    [JsonProperty("Discontinued")]
    public bool? Discontinued { get; set; }

    [JsonProperty("ReorderLevel")]
    public long? ReorderLevel { get; set; }
}

