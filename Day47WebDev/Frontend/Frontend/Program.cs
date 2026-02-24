using BusinessLayer;

class Program
{
    public  static void PrintData()
    {
        BLReverse obj = new BLReverse();

        string x = obj.ReverseString();

        Console.WriteLine(x);
    }

    public static void Main(string[] args)
    {
        PrintData();
    }
}