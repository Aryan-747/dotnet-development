// Functional Class
class TechXam
{
    public string username {get; set;}
    public string password = "TECH_";

    // public methods

    public void GeneratePassword()
    {
        // Converting the username into char array for better operation
        char[] array = username.ToCharArray();

        // Checking First Constraint (First 4 Characters should be UpperCase)
        for(int i=0 ; i<4; i++)
        {

            if(Char.IsLetter(array[i]) && Char.IsUpper(array[i])) // is alphabet & UpperCase
            {
                
            }

            else
            {
                Console.WriteLine($"{username} is invalid"); // Invalid Case Encountered
                return; // Terminate Program
            }
        }

        // Checking 2nd Constraint (5th letter should be '@')

        if(array[4] != '@')
        {
            Console.WriteLine($"{username} is an invalid username");
            return; // terminate program
        }

        // Checking 3rd Constraint (Last 3 Digits should be the courseID that lie between 101 to 115)
        int len = username.Length;
        int num = int.Parse(username.Substring(len-3,3));

        // Valid Case
        if(num>=101 && num<=115)
        {
            
        }

        else
        {
            Console.WriteLine($"{username} is an invalid username");
            return; // Invalid username
        }

        // Generating Password

        // Converting all UpperCase To LowerCase
        int sum = 0;

        for(int i=0 ; i<4 ; i++)
        {
            char x = Char.ToLower(array[i]);
            sum += (int)x;
        }
        // appending the sum into password
        password += sum.ToString();

        // appending last two digits of course id into password
        password += username.Substring(username.Length-2,2);   

        Console.WriteLine(GetPassword()); // Displaying Password After All Checks have Passed
        return;     

    }

    // returns password
    public string GetPassword()
    {
        return password;
    }
    
}

// Main Class
class Program
{
    public static void Main(string[] args)
    {
        string input;
        Console.Write("Enter Your UserName: ");
        input = Console.ReadLine();

        // Base Invalid Case
        if(input.Length<8)
        {
            Console.WriteLine($"{input} is an invalid username");
            return; // Terminate Program
        }

        // using get set to set the username value
        TechXam obj = new TechXam
        {
            username = input,
        };

        // Calling Password Generator Function

        obj.GeneratePassword();
        
    }
}