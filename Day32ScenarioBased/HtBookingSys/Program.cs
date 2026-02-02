using System;

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

    private SortedDictionary<int, Room> RoomList = new SortedDictionary<int, Room>(); // Sorted Dict to Store Rooms

    public void AddRoom(int roomnumber, string type, double price)
    {
        if(RoomList.ContainsKey(roomnumber))
        {
            Console.WriteLine("Room Already Exists!");
            return;
        }

    }

}





class Program
{

}