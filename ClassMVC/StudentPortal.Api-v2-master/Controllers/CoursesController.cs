using Microsoft.AspNetCore.Mvc;
using StudentPortal.Api.DTOs;
using StudentPortal.Api.Services;
namespace StudentPortal.Api.Controllers;
[ApiController, Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    public CoursesController(ICourseService service) => _service = service;
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => (await _service.GetByIdAsync(id)) is { } d ? Ok(d) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(CourseCreateDto dto) { var r = await _service.CreateAsync(dto); return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.CourseId }, r.Data) : BadRequest(r.Message); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CourseUpdateDto dto) { if (id != dto.CourseId) return BadRequest("Route id and body id do not match."); var r = await _service.UpdateAsync(dto); return r.Success ? Ok(r.Message) : NotFound(r.Message); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) { var r = await _service.DeleteAsync(id); return r.Success ? Ok(r.Message) : NotFound(r.Message); }
}