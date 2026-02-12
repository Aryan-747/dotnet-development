class Ques5
{   
    public static void Main()
    {
        string inp = Console.ReadLine();

        if(int.TryParse(inp, out int res))
        {
            Console.Write($"number is: {res}");
        }

        else
        {
            Console.WriteLine("Not a number!");
        }    
    }
}