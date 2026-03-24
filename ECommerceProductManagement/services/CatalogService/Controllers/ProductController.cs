using Microsoft.AspNetCore.Mvc;
using CatalogService.DTOs;
using CatalogService.Models;
using CatalogService.Repositories;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repo.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            SKU = dto.SKU,
            Status = ProductStatus.Draft
        };

        await _repo.Add(product);

        return Ok(product);
    }
}