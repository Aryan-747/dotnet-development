// Main Class
class HeavenHomes
{
    // Dictionary to Store Data
    private Dictionary<string,double> apartmentDetailsMap = new Dictionary<string,double>();

    // public methods
    public void addApartmentDetails(string apartmentNumber, double rent)
    {
        apartmentDetailsMap.Add(apartmentNumber,rent); // Adds Data into Map
    }

    // finds Total Rent in given Range and returns the same
    public double findTotalRentOfApartmentsInTheGivenRange(double minimumRent, double maximumRent)
    {
        double totalRent = 0;

        foreach(var x in apartmentDetailsMap)
        {
            if(x.Value>=minimumRent && x.Value<=maximumRent)
            {
                totalRent += x.Value;
            }
        }

        return totalRent;

    }
}

// User Interface Class
class Program
{
    public static void Main(string[] args)
    {
        // Initializing Object of Apartment class
        HeavenHomes obj = new HeavenHomes();

        // Taking User Inputs
        int n;
        Console.Write("Enter number of apartments you want to enter: ");
        n = int.Parse(Console.ReadLine());

        Console.WriteLine("---Enter Apartment Details---");
        for(int i=0 ; i<n ; i++)
        {
            string input;
            input = Console.ReadLine();

            string[] inparr = input.Split(":");
            obj.addApartmentDetails(inparr[0],Double.Parse(inparr[1])); // adding data into Map
        }

        double minrent;
        double maxrent;

        Console.WriteLine("---Enter the range to filter the details---");
        minrent = double.Parse(Console.ReadLine());
        maxrent = double.Parse(Console.ReadLine());

        double totalrent = obj.findTotalRentOfApartmentsInTheGivenRange(minrent,maxrent);

        if(totalrent == 0)
        {
            Console.WriteLine("No Apartments Found in This Range");
        }

        else
        {
        Console.WriteLine($"Total Rent in the Range {minrent:F1} to {maxrent:F1} USD:{totalrent:F1}");
        }
    }
}