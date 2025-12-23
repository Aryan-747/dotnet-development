using System;

public class Height
{

    public static void Main()
    {
        // taking input of height
        string input;
        Console.Write("Enter the height: ");
        input = Console.ReadLine();


        // checking if height is valid intger (i.e is parseable)
        if(int.TryParse(input, out int height))
        {
            if(height<150)
            {
                Console.WriteLine("Dwarf!");
            }

            else if(height>=150 && height<165)
            {
                Console.WriteLine("Average!");
            }

            else if(height>=165 && height<=190)
            {
                Console.WriteLine("Tall!");
            }

            else
            {
                Console.WriteLine("Abnormal!");
            }
        }
        

        // asking again, till input is valid.
        else
        {
            Console.WriteLine("Enter valid input please!");
            Main();
        }
    }
}