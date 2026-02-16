class Program
{
    public static HashSet<int> getFirstOccurence(List<int> scans)
    {
        HashSet<int> result = new HashSet<int>();
        for(int i=0; i<scans.Count; i++)
        {
            result.Add(scans[i]);
        }

        return result;
    }

    public static void Main(string[] args)
    {
        List<int> scans = new List<int>();
        int n;
        n = int.Parse(Console.ReadLine());

        for (int i=0; i<n; i++)
        {
            int val = int.Parse(Console.ReadLine());
            scans.Add(val);
        }

       HashSet<int> firstonly = getFirstOccurence(scans);
      
       for (int i = 0; i < firstonly.Count; i++)
       {
            Console.Write($"{firstonly.ElementAt(i)} ");
       }

        // Prevents termination
        Console.ReadKey();
    }
}