using BlazorAp.Models;
using System.ComponentModel;

namespace BlazorAp.Services
{
    public class CartService : INotifyPropertyChanged
    {
        private List<CartItem> _cartItems = new List<CartItem>();

        public List<CartItem> CartItems => _cartItems;

        public int TotalItems => _cartItems.Sum(item => item.Quantity);

        public decimal TotalPrice => _cartItems.Sum(item => item.Total);

        public string FormattedTotalPrice => TotalPrice.ToString("C");

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddToCart(Product product)
        {
            var existingItem = _cartItems.FirstOrDefault(item => item.Sku == product.Sku);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    Sku = product.Sku,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            
            OnPropertyChanged();
        }

        public void UpdateQuantity(string sku, int quantity)
        {
            var item = _cartItems.FirstOrDefault(i => i.Sku == sku);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    RemoveFromCart(sku);
                }
                else
                {
                    item.Quantity = quantity;
                    OnPropertyChanged();
                }
            }
        }

        public void RemoveFromCart(string sku)
        {
            _cartItems.RemoveAll(item => item.Sku == sku);
            OnPropertyChanged();
        }

        public void ClearCart()
        {
            _cartItems.Clear();
            OnPropertyChanged();
        }

        public bool IsInCart(string sku)
        {
            return _cartItems.Any(item => item.Sku == sku);
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
