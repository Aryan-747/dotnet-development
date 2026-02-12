using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        string input = "1,234.56";

        decimal number = decimal.Parse(input, 
                        NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, 
                        CultureInfo.InvariantCulture);

        Console.WriteLine(number);   // Output: 1234.56
    }
}
