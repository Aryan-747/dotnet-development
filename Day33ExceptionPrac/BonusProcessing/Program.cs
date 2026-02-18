using System;
using System.Net;
using System.Threading.Channels;

class Program
{
    static void Main()
    {
        int[] salaries = { 5000, 0, 7000 };

        // TODO:
        // 1. Loop through salaries
        // 2. Divide bonus by salary
        // 3. Handle DivideByZeroException
        // 4. Continue processing remaining employees

        int bonus = 50000;


        foreach(int salary in salaries)
        {
            try
            {
                Console.WriteLine("Divded Bonus is: " + (bonus/salary));
                
            }

            catch(DivideByZeroException)
            {
                Console.WriteLine("Error: Cannot Divide By Zero!");
            }
        }

        Console.WriteLine("Program Executed!");


    }
}