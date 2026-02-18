using System;

class LoginException : Exception
{
    public LoginException(string message) : base(message)
    {
        
    }
}


class Program
{
    static void Main()
    {
        int attempts = 0;
        string password = "admin123";

        // TODO:
        // 1. Allow only 3 login attempts
        // 2. Create and throw custom exception after limit
        // 3. Handle exception and terminate application

        try
        {
            while(attempts<3)
            {
                Console.Write("Enter Password: ");
                string input = Console.ReadLine();

                if(input == password)
                {
                    Console.WriteLine("Login Successfull");
                    return;
                }

                else
                {
                    attempts++;
                    Console.WriteLine($"Incorrect Password! Attempt: {attempts}");
                }
            }

            throw new LoginException("Maximum Login Attempts Reached!");    
        }

        catch(LoginException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Application Terminated!");
        }

        finally
        {
            Console.WriteLine("Login Process Completed!");
        }


    }
}