using System;

// base abstract class
abstract class TaxCalc
{
    public abstract void DisplayMethod();
};

class Indian : TaxCalc
{
    // overriding the abstract method
   public override void DisplayMethod()
    {
        Console.WriteLine("Tax is being calculated as per Indian Tax Regime!");
    }
}

class American : TaxCalc
{
    // overridng the abstract method
    public override void  DisplayMethod()
    {
        Console.WriteLine("Tax is being calculated as per American Tax Regime!");
    }
}

class PracticeQuestion1
{

    public static void Main(string[] args)
    {
        // creating instances of abstract class types
        TaxCalc ip = new Indian();
        TaxCalc ap = new American();

        // displaying data
        ip.DisplayMethod();
        ap.DisplayMethod();
    }
}