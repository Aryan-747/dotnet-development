class Employee
{
    public int Id;
    public string Name;
    public string Department;
    public double Salary;
    public DateOnly JoiningDate;

    // Parameterized Constructor
    public Employee(int id, string name, string dept, double salary, DateOnly joining)
    {
        Id = id;
        Name = name;
        Department = dept;
        Salary = salary;
        JoiningDate = joining;
    }
}

