using System;
using System.Security.Cryptography.X509Certificates;

public class Bike
{
    public string Model {get; set;}
    public string Brand {get; set;}
    public int PricePerDay {get; set;}
}

public class BikeUtility
{
    // Methods
    public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();

    public void AddBikeDetails(int index,string model, string brand, int pricePerDay)
    {
        // Adds Details
        bikeDetails.Add(index,new Bike{Model = model, Brand = brand, PricePerDay = pricePerDay});   
    }

    // Method 2
    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
    {
        SortedDictionary<string, List<Bike>> groupedBikes = new SortedDictionary<string, List<Bike>>();

        foreach (var item in bikeDetails.Values)
        {
            if (!groupedBikes.ContainsKey(item.Brand))
            {
                groupedBikes[item.Brand] = new List<Bike>();
            }

            groupedBikes[item.Brand].Add(item);
        }

        return groupedBikes;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        int choice;
        
        BikeUtility obj = new BikeUtility();
        int index = 1;

        while(true)
        {
            Console.Write("1.Add Bike Details\n2.Group Bikes By Brand\n3.Exit\n");
            Console.WriteLine("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            string model;
            string brand;
            int pricePerday;

            if(choice == 1)
            {
                Console.WriteLine("Enter Bike Details");

                Console.Write("Enter the model: ");
                model = Console.ReadLine();
                Console.Write("Enter the brand: ");
                brand = Console.ReadLine();
                Console.Write("Enter the price per day: ");
                pricePerday = int.Parse(Console.ReadLine());

                obj.AddBikeDetails(index,model,brand,pricePerday);
                index++;
            }

            if(choice == 2)
            {
                SortedDictionary<string, List<Bike>> result = obj.GroupBikesByBrand();
            
                foreach (var x in result)
                {
                    Console.WriteLine(x.Key);

                    foreach (var y in x.Value)
                    {
                        Console.WriteLine(y.Model);
                    }

                    Console.WriteLine();
                }
            }

            if(choice == 3)
            {
                return; // Exit Program
            }
            
        }
        

        
    }
}