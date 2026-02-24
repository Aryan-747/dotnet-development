using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppDemo.Models;

namespace WebAppDemo.Controllers
{
    public class HomeController : Controller
    {

        // IActionResult can be used as a return type for any data type
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
