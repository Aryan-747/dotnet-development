using System;

class Admission {
    // Function that checks if Candidate is Eligible
    public static bool IsEligible(int mathmarks, int phymarks, int chemmarks)
    {
        int total = mathmarks + phymarks + chemmarks;

        if(mathmarks>=65 && phymarks>=55 && chemmarks>=50 && ((total>=180) || ((mathmarks+phymarks)>=140)))
        {
          return true;
        }

        return false;

    }

    // Function for taking user input
    static string GetInput(string message)
    {
        Console.Write(message);
        string inp = Console.ReadLine();
        return inp;
    }


    // Function to convert string input into integer
    static int ProcessInput(string input)
    {
        // converting string input to integer
        try
        {
            if(int.TryParse(input, out int mark1))
            {
                return mark1;
            }

            return 0;
        }

        catch
        {
            return 0;
        }
    }

    public static void Main(string[] args)
    {
        // Taking Math marks input
        string inp1 = GetInput("Enter Marks in Math:");
        int mathmarks = ProcessInput(inp1);

        // Taking Physics marks input
        string inp2 = GetInput("Enter Marks in Physics:");
        int phymarks = ProcessInput(inp2);

        // Taking Chemistry marks input
        string inp3 = GetInput("Enter Marks in Chemistry: ");
        int chemmarks = ProcessInput(inp3);

        // Printing result if student is eligible or not
        Console.WriteLine("The Student is Eligible: " + IsEligible(mathmarks, phymarks, chemmarks));
    }
}