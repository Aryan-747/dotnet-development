using Microsoft.EntityFrameworkCore;
using StudentPortal.Api.Data;
using StudentPortal.Api.Models;
namespace StudentPortal.Api.Repositories;
public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly StudentPortalDbContext _context;
    public EnrollmentRepository(StudentPortalDbContext context) => _context = context;
    public Task<List<Enrollment>> GetAllAsync() => _context.Enrollments.Include(x => x.Student).Include(x => x.Course).OrderBy(x => x.EnrollmentId).ToListAsync();
    public Task<Enrollment?> GetByIdAsync(int id) => _context.Enrollments.Include(x => x.Student).Include(x => x.Course).FirstOrDefaultAsync(x => x.EnrollmentId == id);
    public async Task<Enrollment> AddAsync(Enrollment enrollment) { _context.Enrollments.Add(enrollment); await _context.SaveChangesAsync(); return enrollment; }
    public async Task UpdateAsync(Enrollment enrollment) { _context.Enrollments.Update(enrollment); await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(Enrollment enrollment) { _context.Enrollments.Remove(enrollment); await _context.SaveChangesAsync(); }
    public Task<bool> ExistsForStudentCourseAsync(int studentId, int courseId, int? excludeEnrollmentId = null) => _context.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId && (!excludeEnrollmentId.HasValue || e.EnrollmentId != excludeEnrollmentId.Value));
}