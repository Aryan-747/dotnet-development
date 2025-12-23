using System;

public class SchoolStudent
{
    public string name;
    public int age;
    public int rollno;

    // Taking Input
    public static void GetData()
    {
        Console.Write("Enter Student Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Student Age: ");
        age = int.TryParse(Console.ReadLine(), out age);

        Console.Write("Enter Student RollNo: ");
        rollno = int.TryParse(Console.ReadLine(), out rollno);
    }

    // Displaying Data
    public static void DisplayData()
    {
        Console.WriteLine("School Student Name: " + this.name);
        Console.WriteLine("School Student Age: " + this.age);
        Console.WriteLine("School Student RollNo: " + this.rollno);
    }
}

public class UnderGradStudent : SchoolStudent
{
   
    public static void Main(string[] args)
    {
        SchoolStudent s1 = new SchoolStudent();
        s1.GetData();
        s1.DisplayData();

    }
}

