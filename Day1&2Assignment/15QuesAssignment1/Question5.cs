using System;

public class Leap
{   
    // Function that checks if the year is a leap year or not
    public static bool IsLeap(int year)
    {
        if ((year % 400 == 0) ||  (year%4 == 0 && year%100!=0))
        {
            return true;
        }
        
        return false;
    }

    public static void Main()
    {
        // handling input
        string input;
        Console.Write("Enter a year: ");
        input = Console.ReadLine();

        // checking if input is valid or not using TryParse
        if(int.TryParse(input, out int year) && (year>=1900 && year<=5000))
        {
            Console.WriteLine("The year " + year + " is a leap year: " + IsLeap(year));
        }

        else // invalid input
        {
            Console.WriteLine("Enter a valid year.");
            Main();
        }
    }
}