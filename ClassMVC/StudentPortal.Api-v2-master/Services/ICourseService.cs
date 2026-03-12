using StudentPortal.Api.DTOs;
namespace StudentPortal.Api.Services;
public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<(bool Success, string Message, CourseDto? Data)> CreateAsync(CourseCreateDto dto);
    Task<(bool Success, string Message)> UpdateAsync(CourseUpdateDto dto);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}