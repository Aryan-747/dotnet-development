using System;
using System.Reflection.PortableExecutable;

public class Program
{
    class Student
    {        
        public int id;
        public string name;
        public int age;
        public char gender;


        // Constructors
        #region

        // Default Constructor
        public Student()
        {
            this.id = 1;
            this.name = "Aryan Salaria";
            this.age = 21;
            this.gender = 'M';
        }

        // Parameterized Constructor
        public Student(int id, string name, int age, char gender)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.gender = gender;
        }
        #endregion


    }
    public static void Main(string[] args)
    {
        Student s1 = new Student();

        Console.WriteLine(s1.id);
        Console.WriteLine(s1.name);
        Console.WriteLine(s1.age);
        Console.WriteLine(s1.gender);

        Student s2 = new Student(2, "Rohan Go", 25, 'M');

        Console.WriteLine(s2.id);
        Console.WriteLine(s2.name);
        Console.WriteLine(s2.age);
        Console.WriteLine(s2.gender);
    }                 



}