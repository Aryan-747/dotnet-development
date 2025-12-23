using System;

namespace FamilyOOP
{
    // Base class (Parent)
    class Parent
    {
        // Encapsulation using protected variables
        protected string familyName;
        protected int houseNumber;

        // Constructor
        public Parent(string name, int houseNo)
        {
            familyName = name;
            houseNumber = houseNo;
        }

        // Virtual method (Polymorphism)
        public virtual void ShowDetails()
        {
            Console.WriteLine("Family Name: " + familyName);
            Console.WriteLine("House Number: " + houseNumber);
        }
    }

    // Child class inheriting Parent
    class Father : Parent
    {
        private string occupation;

        public Father(string name, int houseNo, string job)
            : base(name, houseNo)
        {
            occupation = job;
        }

        // Method overriding
        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine("Father's Occupation: " + occupation);
        }
    }

    // Another child class
    class Son : Parent
    {
        private string school;

        public Son(string name, int houseNo, string schoolName)
            : base(name, houseNo)
        {
            school = schoolName;
        }

        // Method overriding
        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine("Son's School: " + school);
        }
    }

    // Main class
    class Program
    {
        static void Main(string[] args)
        {
            Parent father = new Father("Sharma", 101, "Engineer");
            Parent son = new Son("Sharma", 101, "ABC School");

            Console.WriteLine("Father Details:");
            father.ShowDetails();

            Console.WriteLine("\nSon Details:");
            son.ShowDetails();

            Console.ReadLine();
        }
    }
}
