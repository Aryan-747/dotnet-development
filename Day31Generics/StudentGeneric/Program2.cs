    public delegate void Notify();
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public int Marks { get; set; }

        public int CompareTo(Student? other)
        {
            return other.Marks.CompareTo(this.Marks);
        }

        public event Notify OnNotify;

        public void NeedImpovement()
        {
            Console.WriteLine("Need Impovement");
        }

        public void GoodStudent()
        {
            Console.WriteLine("Good Student");
        }

        public void AverageStudent()
        {
            Console.WriteLine("Average Student");
        }

        public void SendNotification(Student s)
        {
            OnNotify = null;

            if (s.Marks <= 500)
            {
                OnNotify = NeedImpovement;
            }
            else if (s.Marks >= 560)
            {
                OnNotify = GoodStudent;
            }
            else
            {
                OnNotify = AverageStudent;
            }
            OnNotify?.Invoke();
        }
    }
    public class Program2
    {
        static void Main(string[] args)
        {
            Student s = new Student();
            List<Student> students = new List<Student>();

            students.Add(new Student{Name="Vardhan",Marks=500});
            students.Add(new Student{Name="Rohan",Marks=490});
            students.Add(new Student{Name="Rahul",Marks=475});
            students.Add(new Student{Name="Roshan",Marks=400});
         
            students.Sort();
            int rank = 1;
            foreach(Student student in students)
            {
                Console.Write($"Rank = {rank++} Name = {student.Name}, Marks = {student.Marks}, Remarks: ");
                s.SendNotification(student);
                Console.WriteLine("--------------------------------------------------------------------");
            }
        }
    }
