using StudentPortal.Repositories;
using StudentPortal.Services;
using StudentPortalDb.Models;

namespace StudentPortalDb.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Student>> SearchAsync(string? q = null) => _repo.GetAllAsync(q);

        public Task<Student?> GetAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<(bool ok, string message)> CreateAsync(Student student)
        {
            if (string.IsNullOrEmpty(student.FullName))
                return (false, "Full name is required.");
            if (string.IsNullOrEmpty(student.Email))
                return (false, "Email is required.");
            return (true, "Student created successfully.");
        }
    }
}