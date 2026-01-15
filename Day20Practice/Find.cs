using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Find
{
    // Defining a PreConfigured Sorted Dictionary as per question requirements
    public static SortedDictionary<string,long> ItemList = new SortedDictionary<string, long>()
    {
        {"Laptop", 120},
        {"Mobile", 150},
        {"Tablet", 80},
        {"Headphones",300}
    };

    // Functiion to find item by sold count
    public static SortedDictionary<string,long> FindItemByCount(long solditem)
    {
        // return dictionary
        SortedDictionary<string,long> result = new SortedDictionary<string,long>();

        foreach(var item in ItemList)
        {
            if(item.Value == solditem)
            {
                result.Add(item.Key,item.Value);
            }
        }

        // Item Not Found
        if(result.Count() == 0)
        {
            result.Add("No Item With Such Value",0);
            return result;
        }

        return result;
    }

    // Finding Minimum & Maximum In Sorted Dictionary

    public static List<string> FindMinAndMaxItems()
    {
        long MinValue = ItemList.Values.Min();
        long MaxValue = ItemList.Values.Max();

        // List to return Min & Max Values
        List<string> result = new List<string>();

        string MinItem = "";
        string MaxItem = "";

        foreach(var items in ItemList)
        {
            if(items.Value == MaxValue)
            {
                MaxItem = items.Key;                
            }

            if(items.Value == MinValue)
            {
                MinItem = items.Key;
            }
        }

        // adding Min & Max Values to the List & returning it

        result.Add(MinItem);
        result.Add(MaxItem);
        return result;
    }

    // Function to Sort Dictionary By Item Count
    public static Dictionary<string,long> SortByItemCount()
    {
        Dictionary<string,long> SortedByItemCount = 
            ItemList.OrderBy(x => x.Value).ToDictionary(x => x.Key, x=>x.Value);
        
        return SortedByItemCount;
    }


    // Main Function
    public static void Main(string[] args)
    {

        // Displaying Initial Dictionary
        Console.WriteLine("---Initial Dictionary---");
        foreach(var item in ItemList)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
        Console.WriteLine();

        //Taking ItemCount input and passing it into function
        Console.Write("Enter the number of items sold: ");
        long itemcount = long.Parse(Console.ReadLine());

        var DictBySoldCount = FindItemByCount(itemcount);

        // Displaying Dict Contents with the entered itemcount
        Console.WriteLine("Items with Sold Count: " + itemcount);
        foreach(var item in DictBySoldCount)
        {
            Console.WriteLine(item.Key + " : " + item.Value);
        }
        Console.WriteLine();

        // Finding Min & Max And Displaying The Values

        List<string> res = FindMinAndMaxItems();

        Console.WriteLine("Minimum Sold Item: " + res[0]); // Displaying Minimum
        Console.WriteLine("Maximum Sold Item: " + res[1]); // Displaying Maximum

        // Sorting The Dictionary By Number Of Items Sold And Displaying
        Dictionary<string,long> SortedDict = SortByItemCount();

        Console.WriteLine("\n---Sorted Dictionary---");
        foreach(var item in SortedDict)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
    }  
}