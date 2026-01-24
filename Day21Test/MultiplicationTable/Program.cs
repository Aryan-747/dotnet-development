using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

class MultiplicationTable
{
    public int[] Table(int n, int m)
    {
        // initializing array of size m
        int[] array = new int[m];

        for(int i=0 ; i<array.Length ; i++)
        {
            array[i] = n*(i+1);
        }

        return array;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        int n,m;

        // Creating Instance of class
        MultiplicationTable obj = new MultiplicationTable();

        // Taking User Inputs
        Console.Write("Enter n: ");
        n = int.Parse(Console.ReadLine());
        Console.Write("Enter m: ");
        m = int.Parse(Console.ReadLine());

        // Storing result 
        int[] result = obj.Table(n,m);

        // Displaying Result

        foreach(int x in result)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }
}