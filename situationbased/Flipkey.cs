using System;
class Flipkey
{
    // Generating password
    public static string CleanseAndInvert(string str)
    {
        str = str.ToLower();

        string outputstring = "";

        if(str.Length<6)
        {
            return "Invalid Input";
        }

        for (int i = 0; i < str.Length; i++)
        {
            // if elements are in alphabet range we move forward
            if (((int)str[i] >= 65 && (int)str[i] <= 90) || ((int)str[i]>=97 && (int)str[i]<=122))
            {
                // removing even ascii value characters
                if ((int)str[i]%2 == 0)
                {
                    
                }

                else
                {
                    outputstring += str[i];
                }
            }

            // else we return empty string
            else
            {
                return "Invalid Input";
            }
        }

        // converting string array to charArray to reverse 
        char[] array = outputstring.ToCharArray();

        // reversing the characters
        Array.Reverse(array);

        // Making even indexed characters upperCase
        for(int i=0; i<array.Length; i++)
        {
            if (i%2 == 0)
            {
                array[i] = Char.ToUpper(array[i]);
            }
        }

        // converting char array back to string
        string finaloutput = new string(array);

        return finaloutput;
    }

    public static void Main(string[] args)
    {
        // Taking User Input
        string? input;
        Console.Write("Enter the Input String: ");
        input = Console.ReadLine()!;

        // Passing input into the method for updated output and storing in new variable
        string newstring = CleanseAndInvert(input);

        // Displaying output
        Console.WriteLine("The Final Output is: " + newstring);
    }
}