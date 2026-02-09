using System.Threading.Tasks;

public class GreetingService
{
    public static async Task<string> GetGreetingAsync(string name)
    {
        await Task.Delay(2000); // pretend network delay
        return $"Hello, {name}!";
    }

    static async Task Main()
    {
        string str = await GetGreetingAsync("Aryan");
        Console.WriteLine(str);
    }
}