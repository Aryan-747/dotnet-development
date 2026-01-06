// Armstrong Number
class Question3
{
    static bool IsArmstrong(int number)
    {
        int power = 0;

        // finding number of digits in number
        int nn = number;
        
        while(nn>0)
        {
            power++;
            nn = nn / 10;
        }

        // finding sum of digits
        int digsum = 0;
        int dummynum = number;

        while(dummynum>0)
        {
            int digit = dummynum % 10;
            digsum += (int)Math.Pow(digit,power); // converting double value to int and adding up
            dummynum = dummynum / 10;
        }

        if(digsum == number)
        {
            return true; // is Armstrong Number
        }


        return false;
    }

    public static void Main(string[] args)
    {
        // Taking Input
        string input;
        Console.Write("Enter the number you want to check: ");
        input = Console.ReadLine();

        // Parsing & Passing number to function

        if(int.TryParse(input, out int number))
        {
            Console.WriteLine("The number is an armstrong number: " + IsArmstrong(number));
        }

        else
        {
            Console.WriteLine("Invalid Input!");
        }
    }
}