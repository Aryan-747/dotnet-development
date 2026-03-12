using StudentPortal.Api.Models;
namespace StudentPortal.Api.Repositories;
public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student> AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
    Task<bool> EmailExistsAsync(string email, int? excludeStudentId = null);
}