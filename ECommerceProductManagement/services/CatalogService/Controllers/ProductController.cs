using CatalogService.DTOs;
using CatalogService.Models;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
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

    [AllowAnonymous]
    [HttpGet("preview")]
    public async Task<IActionResult> GetPublished()
    {
        return Ok(await _repo.GetPublished());
    }

    [AllowAnonymous]
    [HttpGet("preview/search")]
    public async Task<IActionResult> SearchPublished(
        [FromQuery] string? q,
        [FromQuery] string? category,
        [FromQuery] string? sort)
    {
        return Ok(await _repo.SearchPublished(q, category, sort));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _repo.GetById(id);
        return product == null ? NotFound() : Ok(product);
    }

    [AllowAnonymous]
    [HttpGet("preview/{id:guid}")]
    public async Task<IActionResult> GetPreview(Guid id)
    {
        var product = await _repo.GetById(id);
        return product == null || !product.IsPublished ? NotFound() : Ok(product);
    }

    [Authorize(Roles = "Admin,ProductManager,ContentExecutive")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductUpsertDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            SKU = dto.SKU,
            Brand = dto.Brand,
            Description = dto.Description,
            CategoryName = dto.CategoryName,
            SeoTitle = dto.SeoTitle,
            SeoDescription = dto.SeoDescription,
            Tags = dto.Tags,
            PrimaryImageUrl = dto.PrimaryImageUrl,
            SellingPrice = dto.SellingPrice,
            StockQuantity = dto.StockQuantity,
            Status = ProductStatus.Draft,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.Add(product);
        return Ok(product);
    }

    [Authorize(Roles = "Admin,ProductManager,ContentExecutive")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductUpsertDto dto)
    {
        var product = await _repo.GetById(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = dto.Name;
        product.SKU = dto.SKU;
        product.Brand = dto.Brand;
        product.Description = dto.Description;
        product.CategoryName = dto.CategoryName;
        product.SeoTitle = dto.SeoTitle;
        product.SeoDescription = dto.SeoDescription;
        product.Tags = dto.Tags;
        product.PrimaryImageUrl = dto.PrimaryImageUrl;
        product.SellingPrice = dto.SellingPrice;
        product.StockQuantity = dto.StockQuantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _repo.Update(product);
        return Ok(product);
    }

    [Authorize(Roles = "Admin,ProductManager")]
    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, ProductStatusUpdateDto dto)
    {
        var product = await _repo.GetById(id);

        if (product == null)
        {
            return NotFound();
        }

        if (!Enum.TryParse<ProductStatus>(dto.Status, true, out var status))
        {
            return BadRequest("Invalid product status.");
        }

        product.Status = status;
        product.IsPublished = dto.IsPublished;
        product.UpdatedAt = DateTime.UtcNow;

        await _repo.Update(product);
        return Ok(product);
    }
}
