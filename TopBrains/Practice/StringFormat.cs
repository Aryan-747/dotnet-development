using System.Data;
using System.Text.Json;
public record Student(string Name, int Score);
class StringFormat
{
    public static void Main(string[] args)
    {
        int n;
        Console.Write("Enter number of inputs: ");
        n = int.Parse(Console.ReadLine());

        List<Student> students = new List<Student>();

        Console.WriteLine("Enter input strings in format (Name:Score)");
        while(n>0)
        {
            string inp = Console.ReadLine();
            string[] ere = inp.Split(':');

            students.Add(new Student(ere[0],int.Parse(ere[1])));
            n--;
        }

        int minScore;
        Console.Write("Enter Min Marks: ");
        minScore = int.Parse(Console.ReadLine());

         var filteredAndSorted = students
            .Where(s => s.Score >= minScore)
            .OrderByDescending(s => s.Score)
            .ThenBy(s => s.Name)
            .ToList();

        string res = JsonSerializer.Serialize(filteredAndSorted);
        Console.WriteLine(res);
        /*
        // Printing all Students in List
        Console.WriteLine("---- Student List ----");
        foreach(Student stu in students)
        {
            Console.WriteLine($"Name: {stu.Name}, Score: {stu.Score}");
        }
        */
    }

}