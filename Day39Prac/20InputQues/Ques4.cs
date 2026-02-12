class Ques4
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();

        string[] inparr = input.Split(' ');

        double[] dubarr = new double[inparr.Length];

        for(int i=0 ; i<dubarr.Length; i++)
        {
            dubarr[i] = double.Parse(inparr[i]);
        }

        Console.WriteLine("Double Array is: ");

        for(int i=0 ; i<dubarr.Length; i++)
        {
            Console.WriteLine($"{dubarr[i].GetType()} : {dubarr[i]}");
        }
        
    }
}