using System.Collections;
using System.Data;

class CakeOrder
{   
    // Private Dict to Store OrderId and CakeCost
    private SortedDictionary<string,double> OrderMap = new SortedDictionary<string, double>();

    // Methods
    public void addOrderDetails(string orderId, double cakeCost)
    {
        OrderMap.Add(orderId,cakeCost);
    }

    public SortedDictionary<string,double> findOrdersAboveSpecifiedCost(double cakeCost)
    {
        // Stores Result
        SortedDictionary<string,double> result = new SortedDictionary<string, double>();

        foreach(var x in OrderMap)
        {
            if(x.Value>=cakeCost)
            {
                result.Add(x.Key,x.Value);
            }
        }
        // TODO : Add Functionality to return dictionary in sorted form
        // returning result
        return result;
    }
}

// Main Class (User Interface Class)
class Program
{
    public static void Main(string[] args)
    {   
        // Taking Input (Number of Orders)
        int numoforders = 0;
        Console.Write("Enter number of orders you want to enter: ");
        numoforders = int.Parse(Console.ReadLine());

        // Initializing object of CakeOrder Class
        CakeOrder obj = new CakeOrder();
        Console.WriteLine("Enter Order Details:");
        for(int index = 0 ; index<numoforders ; index++)
        {
            string input = Console.ReadLine();
            string[] inp = input.Split(":");
            obj.addOrderDetails(inp[0],double.Parse(inp[1]));
        }

        double CostCheck;
        Console.Write("Enter the cost to search Cake Orders: ");
        CostCheck = double.Parse(Console.ReadLine());

        SortedDictionary<string,double> OrdersFound = obj.findOrdersAboveSpecifiedCost(CostCheck);

        if(OrdersFound.Count() == 0)
        {
            Console.WriteLine("No Cake Orders Found!");
        }

        else
        {
            Console.WriteLine("Cake Orders Above the Specifed Cost");
            foreach(var x in OrdersFound)
            {
                Console.WriteLine($"Order ID: {x.Key}, Cake Cost: {x.Value}");
            }
        }
    }   
}