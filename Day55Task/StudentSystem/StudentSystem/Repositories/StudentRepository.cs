using Microsoft.EntityFrameworkCore;
using StudentSystemDb.Models;
using StudentSystem.Models;
using StudentSystem.Repositories;

namespace StudentPortal.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentSystemDbContext _db;

        public StudentRepository(StudentSystemDbContext db)
        {
            _db = db;
        }

        public async Task<List<Student>> GetAllAsync(string? q = null)
        {
            var query = _db.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim().ToLower();
                query = query.Where(s =>
                    s.FullName.ToLower().Contains(q) ||
                    s.Email.ToLower().Contains(q));
            }

            // Read-only list -> AsNoTracking improves performance
            return await query.AsNoTracking().OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _db.Students.FindAsync(id);
        }
    }
}