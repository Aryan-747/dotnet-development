using Microsoft.AspNetCore.Mvc;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendNotification()
        {
            return Ok("Notification sent to user");
        }
    }
}