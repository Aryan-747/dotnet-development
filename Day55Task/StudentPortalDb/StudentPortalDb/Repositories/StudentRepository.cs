using Microsoft.EntityFrameworkCore;
using StudentPortalDb.Models;

namespace StudentPortal.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentPortalDbContext _db;

        public StudentRepository(StudentPortalDbContext db)
        {
            _db = db;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _db.Students.SingleOrDefaultAsync();
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
    }
}