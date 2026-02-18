using System;

class InputHandler
{
    static void Main()
    {
        // TODO:
        // 1. Read input from user
        // 2. Handle invalid numeric input
        // 3. Keep asking until valid number is entered

        while(true)
        {
            try
            {
                int num = int.Parse(Console.ReadLine());
                Console.WriteLine($"Number entered is {num}");
                break;
            }

            catch(FormatException)
            {
                Console.WriteLine("Error: Enter valid input!");
            }

            catch(OverflowException) 
            {
                Console.WriteLine("Error: Too Big for integer.");
            }
        }

        Console.WriteLine("Program Ended!");
    }
}
