using CatalogAPI.Services;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ProductService _service;

        public CatalogController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_service.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_service.GetProduct(id));
        }
    }
}