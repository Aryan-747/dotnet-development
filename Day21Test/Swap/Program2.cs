// Swapping with Out
// In this original variables dont get changed, rather a copy gets changed

using System;

class Program2
{
    static void SwapUsingOut(int a, int b, out int x, out int y)
    {
        a = a + b;
        b = a - b;
        a = a - b;

        x = a;
        y = b;
    }

    static void Main()
    {
        int m = 10;
        int n = 20;

        int p, q;

        Console.WriteLine("Before Swap: m = " + m + ", n = " + n);

        SwapUsingOut(m, n, out p, out q);

        Console.WriteLine("After Swap: m = " + p + ", n = " + q);
    }
}
