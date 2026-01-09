namespace Delegates
{
public class Program
{
    public static void Main(string[] args)
    {
        PrintingCompany printingCompany = new PrintingCompany();
        printingCompany.CustomerChoicePrintMessage = new printMessage(Method1);
        printingCompany.Print("Hello, Delegates!");
    }

    private static string Method1(string message)
    {
        return $"Welcome to the delegate method: {message}";
    }

    //remove later on
    private static string Method2(string message)
    {
        return $"Welcome to the delegate method2: {message}";        
    }
}

}