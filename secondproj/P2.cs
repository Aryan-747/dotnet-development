using System;

public class P2
{   public static void Main()
    {
        string age;
        Console.Write("Enter age: ");
        age = Console.ReadLine();

        if(int.TryParse(age, out int newage))
        {
            bool isadult = (newage>=18);
            Console.WriteLine("Adult? " + isadult);
        }

        else
        {
            Console.WriteLine("Invalid age, Please enter a valid number");
        }

    }
    
}