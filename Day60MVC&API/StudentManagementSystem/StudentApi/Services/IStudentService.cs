using StudentApi.Models;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        void AddStudent(Student student);
        Student GetStudentById(int id);
        void UpdateStudent(int id, Student student);
        void DeleteStudent(int id);
    }
}