class Program
{
    public static Dictionary<string,int> Consolidation(List<KeyValuePair<string,int>> itemList)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        // summing the quantities

        foreach(var item in itemList)
        {
            if(result.ContainsKey(item.Key))
            {
                // adding quantity
                result[item.Key] += item.Value;
            }

            else
            {
                // value must be valid
                if(item.Value>0)
                {
                    result.Add(item.Key, item.Value);
                }
            }

        }

        return result;
    }
    public static void Main(string[] args)
    {
        List<KeyValuePair<string,int>> ItemList = new List<KeyValuePair<string,int>>();
        // Adding Items
        ItemList.Add(new KeyValuePair<string, int>("A101",2));
        ItemList.Add(new KeyValuePair<string, int>("B205",1));
        ItemList.Add(new KeyValuePair<string, int>("A101",3));
        ItemList.Add(new KeyValuePair<string, int>("C111",-1));
        ItemList.Add(new KeyValuePair<string, int>("C111", 15));
        ItemList.Add(new KeyValuePair<string, int>("C111", 7));

        Dictionary<string, int> final = Consolidation(ItemList);

        foreach(var x in final)
        {
            Console.WriteLine($"{x.Key} : {x.Value}");
        }
    }
}