using Microsoft.EntityFrameworkCore;
using StudentPortal.Api.Data;
using StudentPortal.Api.Models;
namespace StudentPortal.Api.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly StudentPortalDbContext _context;
    public CourseRepository(StudentPortalDbContext context) => _context = context;
    public Task<List<Course>> GetAllAsync() => _context.Courses.OrderBy(x => x.CourseId).ToListAsync();
    public Task<Course?> GetByIdAsync(int id) => _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
    public async Task<Course> AddAsync(Course course) { _context.Courses.Add(course); await _context.SaveChangesAsync(); return course; }
    public async Task UpdateAsync(Course course) { _context.Courses.Update(course); await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(Course course) { _context.Courses.Remove(course); await _context.SaveChangesAsync(); }
}