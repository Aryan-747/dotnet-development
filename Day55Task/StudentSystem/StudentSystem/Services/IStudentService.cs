using StudentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    public interface IStudentService
    {
        Task<List<Student>> SearchAsync(string? q = null);

        //Task<Student?> GetAsync(int id);

        Task<(bool ok, string message)> CreateAsync(Student student);

        //Task<(bool ok, string message)> UpdateAsync(Student student);

        //Task DeleteAsync(int id);
    }
}