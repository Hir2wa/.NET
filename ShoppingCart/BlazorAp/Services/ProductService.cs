using BlazorAp.Models;

namespace BlazorAp.Services
{
    public class ProductService
    {
        public List<Product> GetProducts()
        {
            // Simple hardcoded products using your local images
            return new List<Product>
            {
                new Product 
                { 
                    Sku = "item0001", 
                    Name = "Classic Widget", 
                    Price = 19.99m,
                    ImagePath = "/images/item0001.jpg",
                    Description = "A high-quality classic widget perfect for everyday use."
                },
                new Product 
                { 
                    Sku = "item0002", 
                    Name = "Premium Widget", 
                    Price = 29.99m,
                    ImagePath = "/images/item0002.jpg",
                    Description = "Our premium widget with enhanced features and durability."
                },
                new Product 
                { 
                    Sku = "item0003", 
                    Name = "Deluxe Widget", 
                    Price = 39.99m,
                    ImagePath = "/images/item0003.jpg",
                    Description = "The ultimate deluxe widget with all premium features included."
                }
            };
        }
    }
}
