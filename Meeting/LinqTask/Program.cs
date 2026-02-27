class Employee
{
    public int EmployeeID {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Title {get; set;}
    public DateOnly DOB {get; set;}
    public DateOnly DOJ {get; set;}
    public string City {get; set;}

    public void Display()
    {
        Console.WriteLine($"{EmployeeID},{FirstName},{LastName},{Title},DOB:{DOB},DOJ:{DOJ},{City}");
    }
}

class Program
{
       public static List<Employee> empList = new List<Employee>
            {
                new Employee() {EmployeeID = 1001,FirstName = "Malcolm",LastName = "Daruwalla",Title = "Manager",DOB = DateOnly.Parse("1984-01-02"),DOJ = DateOnly.Parse("2011-08-09"),City = "Mumbai"},
                new Employee() {EmployeeID = 1002,FirstName = "Asdin",LastName = "Dhalla",Title = "AsstManager",DOB = DateOnly.Parse("1984-08-20"),DOJ = DateOnly.Parse("2012-7-7"),City = "Mumbai"},
                new Employee() {EmployeeID = 1003,FirstName = "Madhavi",LastName = "Oza",Title = "Consultant",DOB = DateOnly.Parse("1987-11-14"),DOJ = DateOnly.Parse("2105-12-04"),City = "Pune"},
                new Employee() {EmployeeID = 1004,FirstName = "Saba",LastName = "Shaikh",Title = "SE",DOB = DateOnly.Parse("6/3/1990"),DOJ = DateOnly.Parse("2/2/2016"),City = "Pune"},
                new Employee() {EmployeeID = 1005,FirstName = "Nazia",LastName = "Shaikh",Title = "SE",DOB = DateOnly.Parse("3/8/1991"),DOJ = DateOnly.Parse("2/2/2016"),City = "Mumbai"},
                new Employee() {EmployeeID = 1006,FirstName = "Suresh",LastName = "Pathak",Title = "Consultant",DOB = DateOnly.Parse("11/7/1989"),DOJ = DateOnly.Parse("8/8/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1007,FirstName = "Vijay",LastName = "Natrajan",Title = "Consultant",DOB = DateOnly.Parse("12/2/1989"),DOJ = DateOnly.Parse("6/1/2015"),City = "Mumbai"},
                new Employee() {EmployeeID = 1008,FirstName = "Rahul",LastName = "Dubey",Title = "Associate",DOB = DateOnly.Parse("11/11/1993"),DOJ = DateOnly.Parse("11/6/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1009,FirstName = "Amit",LastName = "Mistry",Title = "Associate",DOB = DateOnly.Parse("8/12/1992"),DOJ = DateOnly.Parse("12/3/2014"),City = "Chennai"},
                new Employee() {EmployeeID = 1010,FirstName = "Sumit",LastName = "Shah",Title = "Manager",DOB = DateOnly.Parse("4/12/1991"),DOJ = DateOnly.Parse("1/2/2016"),City = "Pune"},
            };
    public static void Main(string[] args)
    {
        // Displaying all emps
        Console.WriteLine("\nAll Employees\n");
        foreach(Employee e in empList)
        {
            e.Display();
        }

        Console.WriteLine("\nEmployees NOT in Mumbai:");

        // not in mumbai
        var notMumbai = empList.Where(e => e.City != "Mumbai");
        foreach (var emp in notMumbai)
        {
            emp.Display();
        }

        // title asst manager
        Console.WriteLine("\nAsstManager Employees:");
        var asstManagers = empList.Where(e => e.Title == "AsstManager");
        foreach (var emp in asstManagers)
        {
            emp.Display();
        }

        // joined before date
        Console.WriteLine("\nEmployees joined before 01/01/2015:");
        DateOnly limitDate = new DateOnly(2015, 1, 1);
        var joinedBefore = empList.Where(e => e.DOJ < limitDate);
        foreach (var emp in joinedBefore)
        {
            emp.Display();
        }

        // birth date after 1990
        Console.WriteLine("\nEmployees with birth date after 1/1/1990");
        DateOnly DOBlimit = new DateOnly(1990,1,1);
        var joinedAfter = empList.Where(e=> e.DOB>DOBlimit);
        foreach(Employee emp in joinedAfter)
        {
            emp.Display();
        }


        // display total number of employees
        int totalnum = 0;
        foreach(var emp in empList)
        {
            totalnum++;
        }
        Console.WriteLine($"\nTotal Number Of Employees: {totalnum}\n");

        // total number of emps joined after 1/1/2015
        Console.WriteLine("Joined after 1/1/2015");
        DateOnly dojlimit = new DateOnly(2015,1,1);
        var joinedaft = empList.Where(e=>e.DOJ>dojlimit);
        foreach(Employee emp in joinedaft)
        {
            emp.Display();
        }

        // Displaying total number of emps belonging to Chennai
        var chennaiList = empList.Where(e=>e.City == "Chennai");
        int chennaicount = chennaiList.Count();
        Console.WriteLine($"\n{chennaicount} employees belong to chennai");



    }
}