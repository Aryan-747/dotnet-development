using System.Runtime.CompilerServices;

class ForensicReport
{
    // Stores Data
    private List<KeyValuePair<string,DateOnly>> reportMap = new List<KeyValuePair<string,DateOnly>>();

    // Method 1: Add into map
    public void addReportingDetails(string officername, DateOnly datefilled)
    {
        reportMap.Add(new(officername,datefilled));
    }

    // Method 2: Filter report officers based on Date
    public List<string> getOfficersWhoFiledReportOnDate(DateOnly reportFiledDate)
    {
        List<string> Result = new List<string>();

        foreach(var report in reportMap)
        {
            if(report.Value == reportFiledDate)
            {
                Result.Add(report.Key);        
            }
        }

        return Result;
    }
}

// user interface class
class Program
{
    public static void Main(string[] args)
    {

        ForensicReport manager = new ForensicReport();

        Console.WriteLine("Enter number of reports you want to enter: ");
        int n;
        n = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Details: (Officername:Date)");

        while(n>0)
        {
            string inp = Console.ReadLine();
            string[] inparr = inp.Split(':');

            manager.addReportingDetails(inparr[0],DateOnly.Parse(inparr[1]));
            n--;
        }

        Console.WriteLine("Enter Report Filed Date Based on which you want to filter: ");
        DateOnly reportfileddate = DateOnly.Parse(Console.ReadLine());

        List<string> res = manager.getOfficersWhoFiledReportOnDate(reportfileddate);

        if(res.Count == 0)
        {
            Console.WriteLine($"No one filed reports on {reportfileddate}");
            return;
        }

        Console.WriteLine($"Officers who filed reports on date: {reportfileddate}");

        foreach(string x in res)
        {
            Console.WriteLine(x);
        }
    }
}