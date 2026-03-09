using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;
using System.Net.Http.Json;

namespace StudentMvc.Controllers
{
    public class StudentsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:7016/");

            var students = await client.GetFromJsonAsync<List<Student>>("api/students");

            return View(students);
        }
    }
}