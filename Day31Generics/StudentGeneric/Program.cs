using System;

class GenericStudent<TName, TMarks>
{
    public TName name;
    public TMarks marks;

    // Constructor
    public GenericStudent(TName Name, TMarks Marks)
    {
        name = Name;
        marks = Marks;
    }
}

delegate void ResultNotification(string name, double marks, double average);

class Program
{
    public static double FindAverage(List<GenericStudent<string,double>> students)
    {
        double average = 0;
        double numberofstudents = students.Count();
        double sum = 0;

        foreach(var x in students)
        {
            sum+=x.marks;
        }

        average = sum/numberofstudents;

        return average;
    }

     // Method matching delegate signature
    public static void NotifyFailure(string name, double marks, double average)
    {
        if (marks < average)
        {
            Console.WriteLine($"Message to {name}: You have FAILED (Marks: {marks}, Average: {average})");
        }
    }

    public static void Main()
    {
        // Creating a list to store students
        List<GenericStudent<string, double>> students = new List<GenericStudent<string, double>>();

        students.Add(new GenericStudent<string, double>("Aryan", 490));
        students.Add(new GenericStudent<string, double>("Rohan", 435));
        students.Add(new GenericStudent<string, double>("Sonu", 450));
        students.Add(new GenericStudent<string, double>("Monu", 475));
        students.Add(new GenericStudent<string,double>("Ronu",595));

        foreach(var x in students)
        {
            Console.WriteLine($"{x.name} : {x.marks}");
        }

        double average = FindAverage(students);
        Console.WriteLine($"The Average Marks are: {average}");

        // Assign method to delegate
        ResultNotification notify = NotifyFailure;

        // Notify students who failed
        foreach (var student in students)
        {
            notify(student.name, student.marks, average);
        }
    }
}