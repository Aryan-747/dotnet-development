using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class LinqStudent
    {
        public string Name { get; set; }

    }
    public class Linq
    {

        public static void Main(string[] args)
        {
            //LinqExample1("DD");
            //LinqExample2();
            LinqExample3();
        }

        private static void LinqExample2()
        {
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new MyProcess() { Name = p.ProcessName, Id= p.Id };
           foreach (var proc in procCollection)
            {
                Console.WriteLine($"Process Name = {proc.Name}  Id = {proc.Id }");
            }
        }


        private static void LinqExample3()
        {
            var procCollection = from p in System.Diagnostics.Process.GetProcesses()
                                 select new  { Name = p.ProcessName, Id = p.Id };
            
            foreach (var proc in procCollection)
            {
                Console.WriteLine($"Process Name = {proc.Name}  Id = {proc.Id}");
            }

            //var procCollection1 = from p in System.Diagnostics.Process.GetProcesses()
            //                     select Math.Max(0,p.Id );
            var maxProcess = System.Diagnostics.Process.GetProcesses().Average(p=>p.Id);
        }


        // Basic Linq Implementation
        public static void  LinqExample1(string name) {

            string[] names = { "A", "B", "C" };

            foreach (var item in names)
            {
                if (item == "B")
                {
                    Console.WriteLine(" B is present");
                }
            }

            var findNames = from nam in names 
                           //where nam == "B"
                           orderby nam ascending
                           select new LinqStudent() { Name = nam };

            foreach (var item in findNames)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
           
        }

        // Pallindrome
        public static string IsPalindrome(string name) {
            if (name.Reverse() == name)
            {
                return "PLINDROME " + name;
            }
            return "not A PLINDROME " + name;
        }

    }
