using System.Collections;
class MeditationCenter
{
    // Base Variables
    public int MemberId {get; set;}
    public int Age {get; set;}
    public double Weight{get; set;}
    public double Height{get; set;}
    public string Goal{get; set;}
    public double BMI {get; set;}

    // Base & Parameterized Constructor

    public MeditationCenter(){}

    public MeditationCenter(int memberid, int age, double weight, double height, string goal, double bmi)
    {
        MemberId = memberid;
        Age = age;
        Weight = weight;
        Height = height;
        Goal = goal;
        BMI = bmi;
    }
}
class Yoga
{
    public static List<MeditationCenter> MemberList = new List<MeditationCenter>();

    public void TakeInput()
    {
        Console.Write("Enter number of members you want to add: ");
        int n = int.Parse(Console.ReadLine());

        int memberid;
        int age;
        double weight;
        double height;
        string goal;
        double bmi;

        while(n>0)
        {
            Console.Write("Enter id: ");
            memberid = int.Parse(Console.ReadLine());
            Console.Write("Enter age: ");
            age = int.Parse(Console.ReadLine());
            Console.Write("Enter weight: ");
            weight = double.Parse(Console.ReadLine());
            Console.Write("Enter height: ");
            height = double.Parse(Console.ReadLine());
            Console.Write("Enter your goal: ");
            goal = Console.ReadLine();
            Console.Write("Enter your BMI: ");
            bmi = double.Parse(Console.ReadLine());

            AddYogaMember(memberid,age,weight,height,goal,bmi);
            n--;
        }
    }

    public void AddYogaMember(int memberid, int age, double weight, double height, string goal, double bmi)
    {
        MemberList.Add(new MeditationCenter(memberid,age,weight,height,goal,bmi));
    }

    public void DisplayList()
    {
        foreach(var x in MemberList)
        {
            Console.WriteLine($"{x.MemberId},{x.Age},{x.Weight},{x.Height},{x.Goal},{x.BMI}");
        }
    }


}
class YogaProgram
{
    public static void Main(string[] args)
    {

        Yoga y1 = new Yoga();
        // Calling Input Function
        y1.TakeInput();
        y1.DisplayList();
    }
}

