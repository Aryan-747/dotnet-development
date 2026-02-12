using System;

class Program
{
    static bool EmailValidator(string email)
    {
        string[] arr = email.Split('@');

        // Invalid email;
        if(arr.Length>2)
        {
            return false;
        }

        // there should be no uppercase chars
        for(int i=0 ; i<email.Length; i++)
        {
            // invalid
            if(Char.IsUpper(email[i]))
            {
                return false;
            }
        }

        // Last 4 chars should be ".com";

        string subem = email.Substring(email.Length-4);

        if(subem != ".com")
        {
            return false;
        }

        return true;
    }

    public static void Main(string[] args)
    {
        string? input = Console.ReadLine();

        if(!string.IsNullOrEmpty(input))
        {
        Console.WriteLine("Entered Email Is Valid: " + EmailValidator(input));
        return;
        }

        Console.WriteLine("Input Cannot Be NULL");

    }
}