using System;

namespace Delegates{

public delegate string PrintMessage(string message);

public class PrintingCompany
{
    public PrintMessage CustomerChoicePrintMessage {get; set;}

    public void Print(string message)
    {
        string messageToPrint = CustomerChoicePrintMessage(message);
        Console.WriteLine(messageToPrint);
    }

    static void MethodA(string msg) => Console.WriteLine("A: " + msg);
    static void MethodB(string msg) => Console.WriteLine("B: " + msg);
    static void MethodC(string msg) => Console.WriteLine("C: " + msg);
    
}
}