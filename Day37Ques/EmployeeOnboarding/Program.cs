class Employee
{

    // Data variables
    public int Id;
    public string Name;
    public string Email;
    public double Salary;


    // Parameterized Constructor to Initialize all Values

    public Employee(int id, string name, string email, double salary)
    {
        // Salary Validation
        if(salary<=0)
        {
            Salary = 30000;
        }

        else
        {
            Salary = salary;
        }
        Id = id;
        Name = name;

        // Email Validation

        bool present = false;
        for(int i=0; i<email.Length; i++)
        {
            if (email[i].Equals('@'))
            {
                present = true;
                break;
            }
        }

        if(present)
        {
            Email = email;
        }
        else
        {
            Email = "unknown@company.com";
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        // Creating 3 Employees

        Employee e1 = new Employee(1, "Rohan", "rohan.com", -500);
        Employee e2 = new Employee(2, "Indra", "indra@gmail.com", 50000);
        Employee e3 = new Employee(3, "Rahul", "rahul123@gmail.com", 45000);

        // Displaying Data

        Console.WriteLine($"{e1.Id}, {e1.Name}, {e1.Email}, {e1.Salary}");
        Console.WriteLine($"{e2.Id}, {e2.Name}, {e2.Email}, {e2.Salary}");
        Console.WriteLine($"{e3.Id}, {e3.Name}, {e3.Email}, {e3.Salary}");


    }
}

