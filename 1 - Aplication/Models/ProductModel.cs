using System.ComponentModel.DataAnnotations;

namespace ShopWebAPI.Aplication.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}