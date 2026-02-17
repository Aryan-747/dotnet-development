class Program
{
    static string SortByFreq(List<string> Codes)
    {
        SortedDictionary<string, int> dict = new SortedDictionary<string, int>();

        foreach(string code in Codes)
        {
            if(dict.ContainsKey(code))
            {
                dict[code]++;
            }

            else
            {
                dict.Add(code, 1);
            }
        }

        var result = dict
                     .OrderByDescending(x => x.Value)
                     .ThenBy(x => x.Key)
                     .First();

        return result.Key;
    }

    public static void Main(string[] args)
    {

        List<string> Codes = new List<string>();
        Codes.Add("E01");
        Codes.Add("E02");
        Codes.Add("E02");
        Codes.Add("E01");
        Codes.Add("E03");

        string result = SortByFreq(Codes);

        Console.WriteLine(result);
    }
}