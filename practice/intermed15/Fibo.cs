using System;

public class Fibo
{

    // function to print first n fibonacci sequence
    public static void PrintSequence(int n)
    {
        int n1 = 0;
        int n2 = 1;

        Console.Write(n1 + " " + n2 + " "); 

        for(int i=0; i<n-2; i++)
        {
            int n3 = n1 + n2;
            Console.Write(n3 + " ");

            n1 = n2;
            n2 = n3;
        }
    }

    public static void Main(string[] args)
    {
        // Taking Inputs
        string inp;
        Console.Write("Enter the number: ");
        inp = Console.ReadLine();

        if(int.TryParse(inp, out int number))
        {
            PrintSequence(number);
        }

        else
        {
            Console.WriteLine("Enter valid input!");
        }

    }


}