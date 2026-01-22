using System;
using System.Data;

class CheckLucky
{

    // Checks if number is prime
    public bool isPrime(int x)
    {
        // edge case
        if(x == 1 )
        {
            return false;
        }

        for(int i=2 ; i<x ; i++)
        {
            if(x%i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public int SumOfDigits(int num)
    {
        int n = num; // making a copy

        int sum = 0;

        while(n>0)
        {
            int dig = n%10; // digit;
            sum += dig;
            n = n /10;    
        }

        return sum; 
    }

    public int IsLucky(int n, int m)
    {
        int count = 0;

        for(int i= n ; i<=m ; i++)
        {
            int sum = SumOfDigits(i);
            int square = i*i;
            int squaresum = SumOfDigits(square);

            if(squaresum == (sum*sum))
            {
                count++;
            }
        }

        return count;
    }


}
class Program
{
    public static void Main(string[] args)
    {
        int n;
        int m;

        Console.Write("Enter lower range (n): ");
        n = int.Parse(Console.ReadLine());
        Console.Write("Enter upper range (m): ");
        m = int.Parse(Console.ReadLine());

        CheckLucky obj1 = new CheckLucky();
        int count = obj1.IsLucky(n,m);
        Console.WriteLine(count);
    }
    
}