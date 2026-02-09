using System;
using System.Collections.Generic;

public class SlidingWindowRateLimiter
{
    private readonly int maxRequests = 5;
    private readonly TimeSpan windowSize = TimeSpan.FromSeconds(10);

    // clientId -> timestamps of requests
    private readonly Dictionary<string, Queue<DateTime>> requestLog = new Dictionary<string, Queue<DateTime>>();

    private readonly object lockObj = new object();

    public bool AllowRequest(string clientId, DateTime now)
    {
        lock (lockObj)
        {
            if (!requestLog.ContainsKey(clientId))
            {
                requestLog[clientId] = new Queue<DateTime>();
            }

            Queue<DateTime> requests = requestLog[clientId];

            // Remove requests outside the sliding window
            while (requests.Count > 0 &&
                   (now - requests.Peek()) > windowSize)
            {
                requests.Dequeue();
            }

            // Check limit
            if (requests.Count >= maxRequests)
            {
                return false;
            }

            // Allow request
            requests.Enqueue(now);
            return true;
        }
    }
}

class Program
{
    static void Main()
    {
        SlidingWindowRateLimiter limiter = new SlidingWindowRateLimiter();
        string clientId = "client-123";

        for (int i = 1; i <= 7; i++)
        {
            bool allowed = limiter.AllowRequest(clientId, DateTime.UtcNow);
            Console.WriteLine($"Request {i}: {(allowed ? "Allowed" : "Blocked")}");
        }
    }
}
