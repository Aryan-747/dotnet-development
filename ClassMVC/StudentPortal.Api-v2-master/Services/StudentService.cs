using StudentPortal.Api.DTOs;
using StudentPortal.Api.Models;
using StudentPortal.Api.Repositories;
namespace StudentPortal.Api.Services;
public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    public StudentService(IStudentRepository repo) => _repo = repo;
    public async Task<List<StudentDto>> GetAllAsync() => (await _repo.GetAllAsync()).Select(Map).ToList();
    public async Task<StudentDto?> GetByIdAsync(int id) => (await _repo.GetByIdAsync(id)) is Student s ? Map(s) : null;
    public async Task<(bool Success, string Message, StudentDto? Data)> CreateAsync(StudentCreateDto dto)
    {
        if (await _repo.EmailExistsAsync(dto.Email)) return (false, "Email already exists.", null);
        var entity = new Student { FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, Status = dto.Status, JoinDate = dto.JoinDate };
        entity = await _repo.AddAsync(entity);
        return (true, "Student created successfully.", Map(entity));
    }
    public async Task<(bool Success, string Message)> UpdateAsync(StudentUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.StudentId);
        if (entity is null) return (false, "Student not found.");
        if (await _repo.EmailExistsAsync(dto.Email, dto.StudentId)) return (false, "Email already exists.");
        entity.FullName = dto.FullName; entity.Email = dto.Email; entity.Phone = dto.Phone; entity.Status = dto.Status; entity.JoinDate = dto.JoinDate;
        await _repo.UpdateAsync(entity); return (true, "Student updated successfully.");
    }
    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return (false, "Student not found.");
        await _repo.DeleteAsync(entity); return (true, "Student deleted successfully.");
    }
    private static StudentDto Map(Student s) => new() 
    { 
        StudentId = s.StudentId, 
        FullName = "Welcome " + s.FullName, 
        Email = s.Email, 
        Phone = s.Phone, 
        Status = s.Status, 
        JoinDate = s.JoinDate, 
        //CreatedAt = s.CreatedAt 
        CreatedAt = DateTime.Now
    };
}