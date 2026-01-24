// Swapping Using Ref
using System;
class Program
{
    static void SwapUsingRef(ref int n1, ref int n2)
    {
        // using third variable for swapping
        int swapper = n1;
        n1 = n2;
        n2 = swapper;
    }

    public static void Main(string[] args)
    {
        int x = 15;
        int y = 25;

        Console.WriteLine($"Before Swap x = {x}, y = {y}");
        SwapUsingRef(ref x, ref y); // Swapping
        Console.WriteLine($"After Swap x = {x}, y = {y}");
    }
}