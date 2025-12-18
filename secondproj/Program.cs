using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Xml.XPath;

class Program
{

    static List<int> sieve( int n)
    {
        bool[] primes = new bool[n+1];

        // initializing prime check array
        for(int i=0 ; i<=n ; i++)
        {
            primes[i] = true;
        }
        

        // marking numbers which are not prime

        for(int p = 2; p*p<=n ; p++)
        {
            if(primes[p])
            {
                for(int i = p*p ; i<=n ; i+=p)
                {
                    primes[i] = false;
                }
            }
        }

        // storing primes in list

        List<int> result = new List<int>();
        for(int i=2 ; i<=n ; i++)
        {
            if(primes[i])
            {
                result.Add(i);
            }
        }

        return result;

    }


    static void Main()
    {
        // finding prime numbers

        int n = 20;

        List<int> res = sieve(n);

        foreach(int ele in res)
        {
            Console.Write(ele + " ");
        }
    }
}