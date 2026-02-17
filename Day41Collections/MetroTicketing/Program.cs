class Program
{
    static int FindCountInPeakRange(Queue<(TimeSpan EntryTime, string TicketType)> q)
    {
        int count = 0;
        TimeSpan PeakStart = new TimeSpan(8, 0, 0);
        TimeSpan PeakEnd = new TimeSpan(10, 0, 0);

        while (q.Count > 0)
        {
            var passenger = q.Dequeue();

            TimeSpan entryTime = passenger.EntryTime;
            string ticketType = passenger.TicketType;

            // Check peak time range (inclusive)
            if (entryTime >= PeakStart && entryTime <= PeakEnd)
            {
                // Check ticket type
                if (ticketType == "Regular")
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static void Main(string[] args)
    {

        Queue<(TimeSpan,string)> q = new Queue<(TimeSpan,string )> ();

        q.Enqueue((new TimeSpan(8, 30, 0), "Regular"));
        q.Enqueue((new TimeSpan(9, 15, 0), "VIP"));
        q.Enqueue((new TimeSpan(10, 0, 0), "Regular"));
        q.Enqueue((new TimeSpan(11, 0, 0), "Regular"));

        int count = FindCountInPeakRange(q);

        Console.WriteLine(count);

    }
}