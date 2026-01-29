using System;

// Base class
class Foresensic
{
    // Dictionary to store data
    private List<KeyValuePair<string, DateOnly>> reportMap = new List<KeyValuePair<string, DateOnly>>();

    // Public Methods

    public void addReportingDetails(string reportingOfficerName, DateOnly reportFiledDate)
    {
        // Adds into Map
        reportMap.Add(new KeyValuePair<string,DateOnly>(reportingOfficerName,reportFiledDate));
    }

    public List<string> getOfficersWhoFiledReportsOnDate(DateOnly reportFiledDate)
    {
        List<string> result = new List<string>();

        foreach(var x in reportMap)
        {
            if(x.Value == reportFiledDate)
            {
                result.Add(x.Key);
            }
        }

        return result;
    }
}


// Implementation Class (Main Class)
class Program
{
    public static void Main(string[] args)
    {
        int n;
        Console.Write("Enter number of reports you want to enter: ");
        n = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter details: ");

        Foresensic obj = new Foresensic();

        for(int i=0; i<n; i++)
        {

            string inp;
            inp = Console.ReadLine();

            string[] inputarray = inp.Split(":");
            obj.addReportingDetails(inputarray[0], DateOnly.Parse(inputarray[1]));
        }

        DateOnly reportFiledDate;

        Console.Write("Enter the date you want to find: ");
        reportFiledDate = DateOnly.Parse(Console.ReadLine());

        List<string> resultarr = obj.getOfficersWhoFiledReportsOnDate(reportFiledDate);

        if(resultarr.Count == 0)
        {
            Console.WriteLine($"No officers filed reports on the date {reportFiledDate}");
        }

        else
        {
            Console.WriteLine($"Officers who filed reports on {reportFiledDate} are: ");
            foreach(string x in resultarr)
            {
                Console.WriteLine(x);
            }
        }

    }
}