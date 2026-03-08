using Microsoft.AspNetCore.Mvc;
using MySwaggerApi.Models;

namespace MySwaggerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    public static List<Employee> employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Narendar", Position = "Developer" },
        new Employee { Id = 2, Name = "Babu", Position = "Designer" },
        new Employee { Id = 3, Name = "Prabhu", Position = "Manager" }
    };
    public static List<string> Data { get; set; } = new List<string>
    {
        "Data Item 1",
        "Data Item 2",
        "Data Item 3"
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new[]
        {
            new { Id = 1, Forecast = "Sunny", Temperature = 32 },
            new { Id = 2, Forecast = "Cloudy", Temperature = 28 },
            new { Id = 3, Forecast = "Rainy", Temperature = 24 }
        });
    }

    [HttpPost]
    public IActionResult AddString(string newString)
    {
        Data.Add(newString);
        return Ok(new { Message = "String added successfully", Data });
    }

    [HttpPost("AddEmployee")]
    public IActionResult AddEmployee(Employee newEmployee)
    {
        employees.Add(newEmployee);
        return Ok(new { Message = "Employee added successfully", Employees = employees });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound(new { Message = "Employee not found" });
        }
        employee.Name = updatedEmployee.Name;
        employee.Position = updatedEmployee.Position;
        return Ok(new { Message = "Employee updated successfully", Employee = employee });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound(new { Message = "Employee not found" });
        }
        employees.Remove(employee);
        return Ok(new { Message = "Employee deleted successfully", Employees = employees });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchEmployee(int id, string empName)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound(new { Message = "Employee not found" });
        }
        if (!string.IsNullOrEmpty(empName))
        {
            employee.Name = empName;
        }

        return Ok(new { Message = "Employee patched successfully", Employee = employee });
    }

    [HttpGet("Employees")]
    public IActionResult GetEmployees()
    {
        return Ok(employees);
    }
}