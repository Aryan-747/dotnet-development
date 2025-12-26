using System;

class BillCalc
{
    public int units;
    public double bill_amount = 0;

    // Constructor
    public BillCalc(int units)
    {
        this.units = units;
    }

    // logic to calculate bill amount
    public double Calculator()
    {

        int unitcntr = 0;
        double billamount = 0;

        while(unitcntr<units)
        {
            if(unitcntr<=199)
            {
                billamount += 1.2;
            }

            else if(unitcntr>=200 && unitcntr<=400)
            {
                billamount += 1.5;
            }

            else if(unitcntr>=401 && unitcntr<=600)
            {
                billamount += 1.8;
            }

            else
            {
                billamount += 2;
            }
            
            unitcntr++;
        }


        // adding surcharge is bill is greater than Rs 400

        if(billamount>400)
        {
            this.bill_amount = billamount;
            return billamount + (billamount*.15);
        }
        this.bill_amount = billamount;
        return billamount;

    }

}

class Question6
{

    public static void Main(string[] args)
    {
        // taking input of units consumed
        string input;
        Console.Write("Enter the number of units consumed: ");
        input = Console.ReadLine();

        
        // parsing into int if valid input and passing value to function

        if (int.TryParse(input, out int units))
        {
            BillCalc user1 = new BillCalc(units);
            double bill_amount = user1.Calculator();

            Console.WriteLine($"Your Bill Amount is: {bill_amount}");
        }

        else
        {
            Console.WriteLine("Invalid Input");
        }
    }
}