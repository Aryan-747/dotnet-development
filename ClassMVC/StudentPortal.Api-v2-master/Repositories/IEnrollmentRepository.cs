using StudentPortal.Api.Models;
namespace StudentPortal.Api.Repositories;
public interface IEnrollmentRepository
{
    Task<List<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<Enrollment> AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task DeleteAsync(Enrollment enrollment);
    Task<bool> ExistsForStudentCourseAsync(int studentId, int courseId, int? excludeEnrollmentId = null);
}