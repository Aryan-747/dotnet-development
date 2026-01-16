using System.Collections;
using System.Runtime.Intrinsics.X86;
class MeditationCenter
{
    // Base Variables
    public int MemberId {get; set;}
    public int Age {get; set;}
    public double Weight{get; set;}
    public double Height{get; set;}
    public string Goal{get; set;}
    public double BMI {get; set;}

}
class YogaProgram
{
    // Declaring Array List to store member objects
    public static ArrayList MemberList = new ArrayList();

    // Function to Add Yoga Members
    public void AddYogaMember(int memberid, int age, double weight, double height, string goal)
    {
        MeditationCenter m = new MeditationCenter
        {
            MemberId = memberid,
            Age = age,
            Weight = weight,
            Height = height,
            Goal = goal,
            BMI = 0
        };

        MemberList.Add(m); // Adding Member Object into Array List
    }

    public double CalculateBMI(int memberid)
    {
        foreach(MeditationCenter Mem in MemberList)
        {
            // Member with Id is found, Hence we calculate BMI
            if(Mem.MemberId == memberid)
            {
                double heightinmet = Mem.Height/100;
                double bmi = Mem.Weight/(heightinmet*heightinmet);
                
                bmi = Math.Round(bmi,2);

                Mem.BMI = bmi;
                return bmi;
            }
        }
        // Member Not Found
        return 0;
    }

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

            AddYogaMember(memberid,age,weight,height,goal);
            n--;
        }
    }

    // Displays Members Currently in List
    public void DisplayDetails()
    {
        Console.WriteLine("\n---Current Members---");
        foreach(MeditationCenter member in MemberList)
        {
            Console.WriteLine($"{member.MemberId},{member.Age},{member.Weight},{member.Height},{member.Goal},{member.BMI}");
        }
        Console.WriteLine();
    }

    public int CalculateYogaFee(int memberid)
    {
        foreach(MeditationCenter mem in MemberList)
        {
            if(memberid == mem.MemberId)
            {
                if(mem.Goal.Equals("Weight Loss"))
                {
                    if(mem.BMI>=25 && mem.BMI<=30)
                    {
                        return 2000;
                    }
                    else if (mem.BMI>=30 && mem.BMI<35)
                    {
                        return 2500;
                    }

                    else if (mem.BMI>=35)
                    {
                        return 3000;
                    }
                }

                else if (mem.Goal.Equals("Weight Gain"))
                {
                    return 2500;
                }
            }
        }
        return 0;
    }

    public static void Main(string[] args)
    {
        YogaProgram p1 = new YogaProgram();
        //p1.TakeInput();

        // Adding Sample Data
        p1.AddYogaMember(105,22,60,178,"Weight Gain");
        p1.AddYogaMember(101,21,70,180,"Weight Loss");

        Console.Write("Enter MemberId you want BMI for: ");
        int memberid = int.Parse(Console.ReadLine());
        double bmi = p1.CalculateBMI(memberid);

        if(bmi == 0)
        {
            Console.WriteLine($"Member with id {memberid} not found!");
        }
        else
        {
            Console.WriteLine("Your Yoga Fee is: " + p1.CalculateYogaFee(memberid));
            p1.DisplayDetails(); // Displays MemberList
        }
    }

}


