using System;

public class Employee
{ 
    public string name;
    public int id;
    public int age;

    // Constructors
    #region
    public Employee(string name, int id, int age)
    {
        this.name = name;
        this.id = id;
        this.age = age;
    }
    #endregion

    // Display
    public void PrintData()
    {
        Console.WriteLine("Name: " + this.name);
        Console.WriteLine("Id: " + this.id);
        Console.WriteLine("Age: " + this.age);
    }
}

public class Competition : Employee
{

    public Competition(string name, int id, int age) : base(name,id,age)
    {

    }
    
    public static void Main(string[] args)
    {
        Employee e1 = new Employee("Aryan",5911,21);
        e1.PrintData();
    }

    public void DisplayResults()
    {

    }
}


