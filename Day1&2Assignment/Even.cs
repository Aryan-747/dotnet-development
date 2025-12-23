using System;

public class Even
{
    #region IsEven Function
    public static bool isEven(int number)
    {
        if(number%2 == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    #endregion

    public static void Main()
    {
        string number;

        Console.Write("Enter the number: ");
        number = Console.ReadLine();

        if(int.TryParse(number, out int newnumber))
        {
           Console.WriteLine("The Number is even: " + isEven(newnumber));
        }

        else
        {
            Console.WriteLine("Enter a valid number!");
            Main();
        }
    }
}