namespace BlazorAp.Models
{
    public class CartItem
    {
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        public decimal Total => Price * Quantity;
    }
}
