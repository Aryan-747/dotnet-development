using StudentPortal.Api.DTOs;
using StudentPortal.Api.Models;
using StudentPortal.Api.Repositories;
namespace StudentPortal.Api.Services;
public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;
    public CourseService(ICourseRepository repo) => _repo = repo;
    public async Task<List<CourseDto>> GetAllAsync() => (await _repo.GetAllAsync()).Select(Map).ToList();
    public async Task<CourseDto?> GetByIdAsync(int id) => (await _repo.GetByIdAsync(id)) is Course c ? Map(c) : null;
    public async Task<(bool Success, string Message, CourseDto? Data)> CreateAsync(CourseCreateDto dto)
    {
        var entity = new Course { Title = dto.Title, DurationDays = dto.DurationDays, Fee = dto.Fee, Level = dto.Level, IsActive = dto.IsActive };
        entity = await _repo.AddAsync(entity); return (true, "Course created successfully.", Map(entity));
    }
    public async Task<(bool Success, string Message)> UpdateAsync(CourseUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.CourseId);
        if (entity is null) return (false, "Course not found.");
        entity.Title = dto.Title; entity.DurationDays = dto.DurationDays; entity.Fee = dto.Fee; entity.Level = dto.Level; entity.IsActive = dto.IsActive;
        await _repo.UpdateAsync(entity); return (true, "Course updated successfully.");
    }
    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return (false, "Course not found.");
        await _repo.DeleteAsync(entity); return (true, "Course deleted successfully.");
    }
    private static CourseDto Map(Course c) => new() { CourseId = c.CourseId, Title = c.Title, DurationDays = c.DurationDays, Fee = c.Fee, Level = c.Level, IsActive = c.IsActive, CreatedAt = c.CreatedAt };
}