using StudentPortal.Api.DTOs;
namespace StudentPortal.Api.Services;
public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<StudentDto?> GetByIdAsync(int id);
    Task<(bool Success, string Message, StudentDto? Data)> CreateAsync(StudentCreateDto dto);
    Task<(bool Success, string Message)> UpdateAsync(StudentUpdateDto dto);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}