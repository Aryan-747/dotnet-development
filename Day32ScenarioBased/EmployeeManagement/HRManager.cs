class HRManager
{
    // Dict to Store Data
    public SortedDictionary<int,Employee> EmployeeList = new SortedDictionary<int,Employee>();

    // Methods

    int id = 1;
    public void AddEmployee(string name, string dept, double salary)
    {
        // Calling Parameterized Constructor to create new instance
        
        EmployeeList.Add(id,new Employee(id,name,dept,salary,DateOnly.FromDateTime(DateTime.Now)));
        id++;
    }

    // Grouping Employees by Dept And returning the dict
    public SortedDictionary<string,List<Employee>> GroupEmployeesByDepartment()
    {
        SortedDictionary<string,List<Employee>> result = new SortedDictionary<string, List<Employee>>(StringComparer.OrdinalIgnoreCase); // Handles Case Sensitivity Issues

        foreach(var k1 in EmployeeList)
        {
            string department = k1.Value.Department;
            
            if(result.ContainsKey(department))
            {
                result[department].Add(k1.Value);
            }

            else
            {
                List<Employee> employees = new List<Employee>();
                employees.Add(k1.Value);
                result.Add(department,employees);
            }
        }

        return result;
        
    }

    
}