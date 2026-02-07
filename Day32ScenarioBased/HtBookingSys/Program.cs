using System;
using System.Collections.Concurrent;

class Room
{
    public int RoomNumber;
    public string RoomType; // Single/Double/Suite
    public double PricePerNight;
    public bool isAvailable;

    // Parameterized Constructor
    public Room(int roomnumber, string roomtype, double pricepernight, bool isavailable)
    {
        RoomNumber = roomnumber;
        RoomType = roomtype;
        PricePerNight = pricepernight;
        isAvailable = isavailable;
    }
}

class HotelManager
{
    public double TotalCost = 0;
    private SortedDictionary<int, Room> RoomList = new SortedDictionary<int, Room>(); // Sorted Dict to Store Rooms
    
    // Adds Room
    public void AddRoom(int roomnumber, string type, double price)
    {
        if(RoomList.ContainsKey(roomnumber))
        {
            Console.WriteLine("Room Already Exists!");
        }

        else
        {
            RoomList.Add(roomnumber,new Room(roomnumber,type,price,true));
            Console.WriteLine($"Room: {roomnumber} Added Successfully!");
        }
    }

    // Calculates Total
    public double CalculateTotal(int roomnumber, int nights)
    {
        double total = 0;

        foreach(var x in RoomList)
        {
            if(x.Key == roomnumber)
            {
               total = x.Value.PricePerNight*nights;
               return total;
            }
        }
        return 0;
    }

    public bool BookRoom(int roomnumber, int nights)
    {
        // Edge Case
        if(RoomList.Count == 0)
        {
            return false; // No Rooms Available
        }


        foreach(var x in RoomList)
        {
            if(x.Key == roomnumber)
            {
                if(x.Value.isAvailable == true)
                    {
                        x.Value.isAvailable = false; // Room Booked
                        TotalCost = CalculateTotal(roomnumber,nights); // calculates totalcost
                        Console.WriteLine($"Room: {roomnumber} Booked Successfully");
                        Console.WriteLine($"Total Cost of Stay: {TotalCost}");
                        return true;
                    }

                    else
                    {
                        Console.WriteLine("Room is Already Booked!");
                        return true;
                    }
            }
        }
        return false; // Room Not found   
    }

    // Groups Rooms By Type & returns Dict
    public Dictionary<string,List<Room>> GroupRoomsByType()
    {
        Dictionary<string,List<Room>> result = new Dictionary<string,List<Room>>();

        foreach(var x in RoomList)
        {
            string roomtype = x.Value.RoomType;

            if(result.ContainsKey(roomtype))
            {
                result[roomtype].Add(x.Value);
            }

            else
            {
                List<Room> list = new List<Room>();
                list.Add(x.Value);
                result.Add(roomtype,list);
            }
        }

        return result;
    }

    // Gets Rooms In Price Range
    public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
    {
        List<Room> result = new List<Room>();

        foreach(var x in RoomList)
        {
            if(x.Value.PricePerNight>=min && x.Value.PricePerNight<=max)
            {
                result.Add(x.Value);
            }
        }

        return result;
    }
}

class Program
{
    // Main Class
    public static void Main(string[] args)
    {
        HotelManager manager = new HotelManager();

        // Handling User Input

        int choice;

        while(true)
        {
            Console.WriteLine("---------- Hotel Booking System ----------");
            Console.WriteLine("\n1. Add Room\n2. Book Room\n3. List Rooms By Type\n4. List Rooms in Budget\n0. Exit\n");
            Console.Write("Enter Choice: ");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {   
                // Add Room
                case 1:
                int roomnumber;
                string type;
                double price;
                Console.Write("Enter Room Number: ");
                roomnumber = int.Parse(Console.ReadLine());
                Console.Write("Enter Room Type: ");
                type = Console.ReadLine();
                Console.Write("Enter Price Per Night: ");
                price = int.Parse(Console.ReadLine());

                manager.AddRoom(roomnumber,type,price);
                break;

                // Book room
                case 2:
                int roomnumbertobook;
                int numberofnights;
                Console.Write("Enter Room Number To Be Booked: ");
                roomnumbertobook = int.Parse(Console.ReadLine());
                Console.Write("Enter number of Nights: ");
                numberofnights = int.Parse(Console.ReadLine());

                bool check = manager.BookRoom(roomnumbertobook,numberofnights);
                if(!check)
                    {
                        Console.WriteLine("Room Does Not Exist!");
                    }
                break;
            

                // Display Rooms By Type
                case 3:

                Console.WriteLine("-------Rooms Grouped By Type-------");
                Dictionary<string,List<Room>> r1 = manager.GroupRoomsByType();

                foreach(var x in r1)
                {
                    Console.WriteLine($"Room Type: {x.Key}");
                    foreach(Room room in x.Value)
                    {
                        Console.WriteLine($"Room: {room.RoomNumber}, Type: {room.RoomType}, PricePerNight: {room.PricePerNight}, IsAvailable: {room.isAvailable}");
                    }
                    Console.WriteLine();
                }
                break;

                // Find Rooms in Budget
                case 4:

                int min;
                int max;
                Console.Write("Enter minimum budget: ");
                min = int.Parse(Console.ReadLine());

                Console.Write("Enter maximum budget: ");
                max = int.Parse(Console.ReadLine());

                List<Room> res = manager.GetAvailableRoomsByPriceRange(min,max);
                foreach(Room x in res)
                 {
                    Console.WriteLine($"Room: {x.RoomNumber} | Type: {x.RoomType} | PricePerNight: {x.PricePerNight} | IsAvailable: {x.isAvailable}");
                 }
                break;

                case 0:
                Console.WriteLine("Program Exited Successfully!");
                return;
                break;

                default:
                Console.WriteLine("Invalid Choice!");
                break;
            }
            
        }

        // Hardcoded data for testing
        /*
        manager.AddRoom(101,"Single",100);
        manager.AddRoom(150,"Double",150);
        manager.AddRoom(300,"Suite",500);
        manager.AddRoom(350,"Suite",500);
        manager.AddRoom(102,"Double",200);
        manager.AddRoom(319,"Suite",550);

        manager.BookRoom(101,15);
        Console.WriteLine($"Total Cost of Stay: {manager.TotalCost}");
        manager.BookRoom(350,5);
        Console.WriteLine($"Total Cost of Stay: {manager.TotalCost}");
        */
    }

}