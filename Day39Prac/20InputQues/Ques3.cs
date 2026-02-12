class Ques3
{
    public static void Main(string[] args)
    {
        string? inp = Console.ReadLine();

        string[] arr = inp.Split(' ');

        int[] intarr = new int[arr.Length];

        for(int i=0 ; i<intarr.Length; i++)
        {
            intarr[i] = int.Parse(arr[i]);
        }

        Console.Write("Array is: ");
        for(int i=0 ; i<intarr.Length; i++)
        {
            Console.Write(intarr[i] + " ");
        }
    }
}