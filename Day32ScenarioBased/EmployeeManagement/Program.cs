class Program
{
    public static void Main(string[] args)
    {
        HRManager manager = new HRManager();

        manager.AddEmployee("Aryan","IT Support",500);
        manager.AddEmployee("Rohan","HR",750);
        manager.AddEmployee("Rahul", "HR", 500);
        manager.AddEmployee("Anjali", "Sales", 250);
        manager.AddEmployee("Kavya", "Sales", 250);


        SortedDictionary<string,List<Employee>> res = manager.GroupEmployeesByDepartment();

        // Displaying Grouped Data
        foreach(var x in res)
        {
            Console.WriteLine($"Department: {x.Key}");

            foreach(Employee emp in x.Value)
            {
                Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}, JoiningDate: {emp.JoiningDate}");
            }
        }
    }
}