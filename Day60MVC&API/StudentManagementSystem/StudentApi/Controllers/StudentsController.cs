using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
        var students = _studentService.GetStudents();

        return Ok(students);
    }

    [HttpPost]
    public IActionResult AddStudent([FromBody] CreateStudentDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Age = dto.Age,
            Course = dto.Course
        };

        _studentService.AddStudent(student);

        return Ok(student);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _studentService.GetStudentById(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student student)
    {
        _studentService.UpdateStudent(id, student);

        return Ok("Student updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        _studentService.DeleteStudent(id);

        return Ok("Student deleted successfully");
    }
}