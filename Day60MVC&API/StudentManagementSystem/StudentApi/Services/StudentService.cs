using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetStudents()
        {
            return _repository.GetStudents();
        }

        public void AddStudent(Student student)
        {
            _repository.AddStudent(student);
        }

        public Student GetStudentById(int id)
        {
            return _repository.GetStudentById(id);
        }

        public void UpdateStudent(int id, Student student)
        {
            _repository.UpdateStudent(id, student);
        }

        public void DeleteStudent(int id)
        {
            _repository.DeleteStudent(id);
        }
    }
}