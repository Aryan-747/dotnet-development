using CartAPI.Models;
using System.Text.Json;

namespace CartAPI.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        private static List<CartItem> cart = new List<CartItem>();

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AddToCart(CartItem item)
        {
            // Call Catalog Service
            var response = await _httpClient.GetAsync(
                $"https://localhost:7195/api/catalog/{item.ProductId}");

            if (!response.IsSuccessStatusCode)
            {
                return "Product not found in catalog";
            }

            cart.Add(item);

            return "Product added to cart";
        }

        public List<CartItem> GetCart()
        {
            return cart;
        }
    }
}