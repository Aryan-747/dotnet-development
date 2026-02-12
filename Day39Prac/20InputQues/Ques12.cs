class Ques12
{   
    public static void Main()
    {
        string input = Console.ReadLine();

        TimeOnly time = TimeOnly.Parse(input);

        int totalMinutes = time.Hour*60 + time.Minute;

        Console.WriteLine($"{time.GetType()} : {totalMinutes}");
    }
}