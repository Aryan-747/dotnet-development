using System;

// Abstract class
abstract class TaxCalculator
{
    // Abstract method, this gets overriden in the child classes
    public abstract double CalculateTax(double salary);

    // Concrete method
    public void DisplayCountry()
    {
        Console.WriteLine("Tax Calculation Based On Country Rules.");
    }
}

// Child class 1
class IndianEmployee : TaxCalculator
{
    public override double CalculateTax(double salary)
    {
        // Assuming 10% tax for Indian employees
        return salary * 0.10;
    }
}

// Child class 2
class AmericanEmployee : TaxCalculator
{
    public override double CalculateTax(double salary)
    {
        // Assuming 20% tax for American employees
        return salary * 0.20;
    }
}

// Main class
class Abstractclass
{
    static void Main()
    {
        TaxCalculator indian = new IndianEmployee();
        TaxCalculator american = new AmericanEmployee();

        double salary = 500000;

        indian.DisplayCountry();
        Console.WriteLine("Indian Employee Tax: " + indian.CalculateTax(salary));

        Console.WriteLine();

        american.DisplayCountry();
        Console.WriteLine("American Employee Tax: " + american.CalculateTax(salary));
    }
}
