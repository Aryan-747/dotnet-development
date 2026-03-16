using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PaymentController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequest request)
        {
            // simulate payment success

            await _httpClient.PostAsync(
                "https://localhost:7052/api/notification",
                null);

            return Ok("Payment Successful & Notification Sent");
        }
    }
}