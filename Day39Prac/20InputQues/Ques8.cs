class Ques8
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();

        // Converting Hex to int

        int num = Convert.ToInt32(input,16);
        Console.WriteLine($"{num.GetType()} : {num}");
        
    }
}