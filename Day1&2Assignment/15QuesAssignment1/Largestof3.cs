using System;

public class Largestof3
{

    public static int LargestNumber(int n1, int n2, int n3)
    {
        // functional logic to find out max of 3 using nested if-else statements
        if (n1 > n2)
        {
            if (n1 > n3)
            {
                return n1;
            }
            else
            {
                return n3;
            }
        }

        else if (n2 > n3)
        {
            if (n2 > n1)
            {
                return n2;
            }
            else
            {
                return n1;
            }
        }

        else if (n1 > n3)
        {
            if (n1 > n2)
            {
                return n1;
            }
            else
            {
                return n2;
            }
        }

        return 0;

    }

    public static void Main()
    {
        // taking inputs
        string inp1;
        string inp2;
        string inp3;

        Console.WriteLine("Enter 3 numbers: ");
        inp1 = Console.ReadLine();
        inp2 = Console.ReadLine();
        inp3 = Console.ReadLine();


        // checking if inputs are valid and proceeding with the function call
        if(int.TryParse(inp1, out int n1) && int.TryParse(inp2, out int n2) && int.TryParse(inp3, out int n3))
        {
            Console.WriteLine("The largest number out of the inputs is: " + LargestNumber(n1, n2, n3));
        }

        else
        {
            Console.WriteLine("Enter valid inputs.");
            Main();
        }


    }

}