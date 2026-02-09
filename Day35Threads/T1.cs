using System;
using System.Threading.Tasks;

class T1
{
    static async Task SaveAsync()
    {
        await Task.Delay(3000); // pretend we saved to DB
        Console.WriteLine("Saved!");
    }

    static async Task<int> GetTotalAsync()
    {
        await Task.Delay(3000); // pretend we calculated
        return 42;
    }

    static async Task Main()
    {
        await SaveAsync(); // Task (no return)
        int total = await GetTotalAsync(); // Task<int> (returns value)
        Console.WriteLine(total);
    }
}