class CalculateNum
{

    // PreConfigured List of Numbers
    public static List<int> NumberList = new List<int>();

    public static void AddNumbers(int numbers)
    {
        NumberList.Add(numbers);
    }

    // Calculates GPA Scored & Returns Double Value
    public static double GetGPAScored()
    {
        double sum = 0;

        // List is empty
        if(NumberList.Count() == 0)
        {
            return -1;
        }

        foreach(int x in NumberList)
        {
            sum += (double)x*3;
        }

        double GPA = sum/((double)NumberList.Count()*3);
        return GPA;
    }

    // Returns Scored Grade After Taking GPA as input
    public static char GetGradeScored(double gpa)
    {
        if(gpa == 10) return 'S';
        if(gpa>=9 & gpa<10) return 'A';
        if(gpa>=8 & gpa<9) return 'B';
        if(gpa>=7 & gpa<8) return 'C';
        if(gpa>=6 & gpa<7) return 'D';
        if(gpa>=5 & gpa<6) return 'E';

        else
        {
            return '#';
        }
    }

    public static void Main(string[] args)
    {
        // Taking Number Input
        Console.Write("Enter the number of input numbers: ");
        int n = int.Parse(Console.ReadLine());
        while(n>0)
        {
            Console.Write("Enter number: ");
            int x = int.Parse(Console.ReadLine());
            AddNumbers(x);
            n--;
        }

        // Displaying GPA
        double GPA = GetGPAScored();
        if(GPA == -1)
        {
            Console.WriteLine("No Numbers Available");
        }
        else
        {
            Console.WriteLine($"GPA is: {GPA:F2}");
            // Getting Grades & Displaying the Grade
            char Grade = GetGradeScored(GPA);

            if(Grade.Equals('#'))
            {
                Console.WriteLine("Invalid GPA");
            }
            else
            {
            Console.WriteLine("Your Grade is : " + Grade);
            }
        }
    }
}