using Microsoft.EntityFrameworkCore;
using StudentPortal.Api.Data;
using StudentPortal.Api.Models;
namespace StudentPortal.Api.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly StudentPortalDbContext _context;
    public StudentRepository(StudentPortalDbContext context) => _context = context;
    public Task<List<Student>> GetAllAsync() => _context.Students.OrderBy(x => x.StudentId).ToListAsync();
    public Task<Student?> GetByIdAsync(int id) => _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
    public async Task<Student> AddAsync(Student student) 
    { 
        _context.Students.Add(student); 
        await _context.SaveChangesAsync(); 
        return student; 
    }
    public async Task UpdateAsync(Student student) { _context.Students.Update(student); await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(Student student) { _context.Students.Remove(student); await _context.SaveChangesAsync(); }
    public Task<bool> EmailExistsAsync(string email, int? excludeStudentId = null) => _context.Students.AnyAsync(s => s.Email == email && (!excludeStudentId.HasValue || s.StudentId != excludeStudentId.Value));
}