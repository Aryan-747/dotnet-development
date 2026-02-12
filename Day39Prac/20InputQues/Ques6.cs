class Ques6
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        string number = "";

        for(int i=0 ; i<input.Length; i++)
        {
            if(Char.IsDigit(input[i]) || input[i] == '.')
            {
                number+=input[i];
            }
        }

        double num = double.Parse(number);

        Console.WriteLine($"{num.GetType()} : {num}");


    }
}