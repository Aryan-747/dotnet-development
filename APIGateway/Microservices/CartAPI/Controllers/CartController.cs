using CartAPI.Services;
using CartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItem item)
        {
            var result = await _service.AddToCart(item);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            return Ok(_service.GetCart());
        }
    }
}