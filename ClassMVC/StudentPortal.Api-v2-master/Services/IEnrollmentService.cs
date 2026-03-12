using StudentPortal.Api.DTOs;
namespace StudentPortal.Api.Services;
public interface IEnrollmentService
{
    Task<List<EnrollmentDto>> GetAllAsync();
    Task<EnrollmentDto?> GetByIdAsync(int id);
    Task<(bool Success, string Message, EnrollmentDto? Data)> CreateAsync(EnrollmentCreateDto dto);
    Task<(bool Success, string Message)> UpdateAsync(EnrollmentUpdateDto dto);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}