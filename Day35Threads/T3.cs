using System;
using System.Threading.Tasks;

class Program
{
    static async Task PrintAfterDelayAsync()
    {
        Console.WriteLine("B1");
        await Task.Delay(500);
        Console.WriteLine("B2");
    }

    static async Task Main()
    {
        Console.WriteLine("A");
        await PrintAfterDelayAsync();
        Console.WriteLine("C");
    }

}

// Expected output order:
// A
// B1
// (pause ~500ms)
// B2
// C