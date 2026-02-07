using System;

public class Seat
{
    public int SeatNo { get; }
    public bool IsBooked { get; set; }
    public string BookedBy { get; set; }

    public Seat(int seatNo)
    {
        SeatNo = seatNo;
        IsBooked = false;
    }
}

public class SeatStore
{
    private readonly Dictionary<int, Seat> seats = new Dictionary<int, Seat>();

    public SeatStore(IEnumerable<int> seatNumbers)
    {
        foreach (int seatNo in seatNumbers)
        {
            seats[seatNo] = new Seat(seatNo);
        }
    }

    public Seat GetSeat(int seatNo)
    {
        return seats.ContainsKey(seatNo) ? seats[seatNo] : null;
    }
}

public class TicketBookingService
{
    private readonly SeatStore seatStore;
    private readonly object lockObj = new object();

    public TicketBookingService(SeatStore seatStore)
    {
        this.seatStore = seatStore;
    }

    public bool BookSeat(int seatNo, string userId)
    {
        lock (lockObj)
        {
            Seat seat = seatStore.GetSeat(seatNo);

            if (seat == null)
                return false;

            if (seat.IsBooked)
                return false;

            seat.IsBooked = true;
            seat.BookedBy = userId;
            return true;
        }
    }
}

class Program
{
    static void Main()
    {
        // Seats created dynamically (no hardcoding)
        List<int> seatNumbers = new List<int>();

        Console.Write("Enter number of seats: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            seatNumbers.Add(i);
        }

        SeatStore seatStore = new SeatStore(seatNumbers);
        TicketBookingService bookingService = new TicketBookingService(seatStore);

        Thread user1 = new Thread(() =>
        {
            bool result = bookingService.BookSeat(1, "UserA");
            Console.WriteLine($"UserA booking result: {result}");
        });

        Thread user2 = new Thread(() =>
        {
            bool result = bookingService.BookSeat(1, "UserB");
            Console.WriteLine($"UserB booking result: {result}");
        });

        user1.Start();
        user2.Start();

        user1.Join();
        user2.Join();

        Console.ReadLine();
    }
}
