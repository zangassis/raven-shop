namespace ShopWebAPI.Domain.Entities
{
    public class Product
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public double Price { get; protected set; }

        public Product(string id, string name, double price)
        {
            Id = id ?? null;
            Name = name;
            Price = price;
        }
    }
}