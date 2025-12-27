// check prime number

class Question2
{
    public static void Main(string[] args)
    {
        string input;
        Console.Write("Enter a number: ");
        input = Console.ReadLine();

        // parsing the string input to a valid integer

        // successfully parsed
        if(int.TryParse(input,out int number))
        {
            // checking if prime or not using brute force approach

            for(int i=2; i<number; i++)
            {
                if(number%i == 0)
                {
                    Console.WriteLine($"{number} is not a prime number!");
                    return;
                }
            }

            Console.WriteLine($"{number} is a prime number!");
        }

        // invalid input
        else
        {
            Console.WriteLine("Invalid Input");
        }

    }
}