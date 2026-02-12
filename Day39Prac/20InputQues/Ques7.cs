class Ques7
{
    public static void Main(string[] args)
    {
        string longinput = Console.ReadLine();
        long number = long.Parse(longinput);

        Console.WriteLine($"{number.GetType()} : {number}");
    }
}