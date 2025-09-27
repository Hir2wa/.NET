namespace BlazorAp.Models
{
    public class Product
    {
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
