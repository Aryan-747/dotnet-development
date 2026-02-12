class Ques9
{
    public static void Main(string[] args)
    {
        string inp = Console.ReadLine();

        string[] inparr = inp.Split(' ');

        double[] array = new double[inparr.Length];

        for(int i=0 ; i<inparr.Length; i++)
        {
            array[i] = double.Parse(inparr[i]);
        }

        Console.WriteLine("Double Array Is: ");
        for(int i=0 ; i<array.Length; i++)
        {
            Console.WriteLine($"{array[i].GetType()} : {array[i]}");
        }
        
    }
}