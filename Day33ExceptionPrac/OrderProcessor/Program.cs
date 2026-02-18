using System;

class InvalidOrderException : Exception
{
    public InvalidOrderException(string message) : base(message)
    {
        
    }
}

class OrderProcessor
{
    static void Main()
    {
        int[] orders = { 101, -1, 103 };

        // TODO:
        // 1. Process each order
        // 2. Throw exception for invalid order ID
        // 3. Ensure one failure does not stop processing

        foreach(int order in orders)
        {
            try
            {
                if(order<=0)
                {
                    throw new InvalidOrderException("Invalid Order ID!!!");
                }

                Console.WriteLine($"Order Id: {order}");
            }

            catch(InvalidOrderException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        Console.WriteLine("Program Terminated!");
    }
}
