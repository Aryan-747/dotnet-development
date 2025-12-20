using System;

public class Program
{

    class Student
    {        
        public int id;
        public string name;
        public int age;
        public char gender;
            
    }

    public static void Main(string[] args)
    {
        Student s1 = new Student();

        s1.id = 1;
        s1.name = "Rohan Roy";
        s1.age = 19;
        s1.gender = 'M';


        Console.WriteLine(s1.id);
        Console.WriteLine(s1.name);
        Console.WriteLine(s1.age);
        Console.WriteLine(s1.gender);
    }



}