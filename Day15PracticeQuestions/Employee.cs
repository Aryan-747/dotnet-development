// Create a class Employee. With employee id, name and age. Use a Constructor and then define a method to display details

using System;

class EmployeeClass
{
    public int id; // employee id
    public string name; // name
    public int age;

    // Parameterized Constructor

    public EmployeeClass(int id, string name, int age)
    {
        this.id = id;
        this.name = name;
        this.age = age;
    }

    // Method to Display Details

    public void Display()
    {
        Console.WriteLine("Employee Details are: ");
        Console.WriteLine("Id: " + id);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
}
class Employee
{
    public static void Main(string[] args)
    {
        EmployeeClass e1 = new EmployeeClass(1,"Aryan",25);

        e1.Display();
    }
    
}