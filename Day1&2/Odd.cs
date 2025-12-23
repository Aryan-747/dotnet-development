using System;

public class Odd
{
    public static bool IsOdd(int number)
    {
        if (number % 2 != 0)
        {
            return true;
        }

        return false;
    }

    public static void Main()
    {
        string number;

        Console.Write("Enter a number: ");
        number = Console.ReadLine();

        if (int.TryParse(number, out int newnumber))
        {
            if (IsOdd(newnumber))
            {
                Console.WriteLine("The Number is Odd!");
            }
            else
            {
                Console.WriteLine("The Number is Even!");
            }
        }

        else
        {
            Console.WriteLine("Enter a valid number!");
        }
    }
}