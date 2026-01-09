using System;
using System.ComponentModel;

class OverLoadMe
{
    // Function 1
    public int Add(int a, int b, int c)
    {
        return a+b+c;
    }

    // OverLoaded Function 1
    public double Add(double a, double b, double c)
    {
        return a+b+c;
    }
    
}
class MethodOverLoading
{
    public static void Main(string[] args)
    {
        OverLoadMe o1 = new OverLoadMe();

        // Calling First Function
        Console.WriteLine( "Int: "+o1.Add(1,2,3));
        
        // Calling OverLoaded Function
        Console.WriteLine("Double: " + o1.Add(5.4,7.5,10.7));
    }

}