using StudentPortal.Api.DTOs;
using StudentPortal.Api.Models;
using StudentPortal.Api.Repositories;
namespace StudentPortal.Api.Services;
public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo; private readonly IStudentRepository _students; private readonly ICourseRepository _courses;
    public EnrollmentService(IEnrollmentRepository repo, IStudentRepository students, ICourseRepository courses) { _repo = repo; _students = students; _courses = courses; }
    public async Task<List<EnrollmentDto>> GetAllAsync() => (await _repo.GetAllAsync()).Select(Map).ToList();
    public async Task<EnrollmentDto?> GetByIdAsync(int id) => (await _repo.GetByIdAsync(id)) is Enrollment e ? Map(e) : null;
    public async Task<(bool Success, string Message, EnrollmentDto? Data)> CreateAsync(EnrollmentCreateDto dto)
    {
        if (await _students.GetByIdAsync(dto.StudentId) is null) return (false, "Student not found.", null);
        if (await _courses.GetByIdAsync(dto.CourseId) is null) return (false, "Course not found.", null);
        if (await _repo.ExistsForStudentCourseAsync(dto.StudentId, dto.CourseId)) return (false, "This student is already enrolled in this course.", null);
        var entity = new Enrollment { StudentId = dto.StudentId, CourseId = dto.CourseId, EnrollDate = dto.EnrollDate, PaymentStatus = dto.PaymentStatus, PaidAmount = dto.PaidAmount };
        entity = await _repo.AddAsync(entity);
        var full = await _repo.GetByIdAsync(entity.EnrollmentId);
        return (true, "Enrollment created successfully.", full is null ? null : Map(full));
    }
    public async Task<(bool Success, string Message)> UpdateAsync(EnrollmentUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.EnrollmentId);
        if (entity is null) return (false, "Enrollment not found.");
        if (await _students.GetByIdAsync(dto.StudentId) is null) return (false, "Student not found.");
        if (await _courses.GetByIdAsync(dto.CourseId) is null) return (false, "Course not found.");
        if (await _repo.ExistsForStudentCourseAsync(dto.StudentId, dto.CourseId, dto.EnrollmentId)) return (false, "Another enrollment already exists for this student and course.");
        entity.StudentId = dto.StudentId; entity.CourseId = dto.CourseId; entity.EnrollDate = dto.EnrollDate; entity.PaymentStatus = dto.PaymentStatus; entity.PaidAmount = dto.PaidAmount;
        await _repo.UpdateAsync(entity); return (true, "Enrollment updated successfully.");
    }
    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return (false, "Enrollment not found.");
        await _repo.DeleteAsync(entity); return (true, "Enrollment deleted successfully.");
    }
    private static EnrollmentDto Map(Enrollment e) => new() { EnrollmentId = e.EnrollmentId, StudentId = e.StudentId, StudentName = e.Student?.FullName ?? string.Empty, CourseId = e.CourseId, CourseTitle = e.Course?.Title ?? string.Empty, EnrollDate = e.EnrollDate, PaymentStatus = e.PaymentStatus, PaidAmount = e.PaidAmount, CreatedAt = e.CreatedAt };
}