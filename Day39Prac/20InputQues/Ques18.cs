class Ques18
{   
    public static void Main()
    {
        string input = Console.ReadLine();

        string[] inparr = input.Split(' ');

        int[] intarr = new int[15];

        int sum = 0;
        
        for(int i=0 ; i<inparr.Length; i++)
        {
            if(int.TryParse(inparr[i], out int num))
            {
                sum+=num;
            }
        }

        Console.WriteLine($"Sum is: {sum}");
    }
}