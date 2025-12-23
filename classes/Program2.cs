using OopsSessions;

public class Program
{
    public static void Main(string[] args)
    {
        #region Ex1
        //Student student = new Student();
        //student.Id = 1;
        //student.Name = "Arun";

        //Student student1 = new Student() 
        //{ 
        //    Id = 10, 
        //    Name = "Babu" 
        //};

        //string studentDetails = student1.GetDetails();
        //Console.WriteLine(studentDetails);
        #endregion

        Program program = new Program();

        Person person = new Person() { Id = 1, age = 20, Name = "Arul" };
        program.GetDetails(person);

        Man man = new Man() { Id = 10, Name = "Kumar", age = 24, Playing = "Football" };

        Woman woman = new Woman() { Id = 10, Name = "Kumari", age = 24, PlayMange = "Rugby and Home" };

        Person person = woman;

        Person childObject = new Child() { age = 1, Id = 100, Name = "Anki", WatchingCartoon = "Chota Bheem" };

        program.GetDetails(person);

        program.GetDetails(childObject);
    }

    public string GetManDetails(Man input)
    {
        return $" Id = {input.Id}  Name = {input.Name}";
    }

    public string GetWomanDetails(Woman input)
    {
        return $" Id = {input.Id}  Name = {input.Name}";
    }

    public string GetDetails(Person person)
    {
        return $" Id = {person.Id}  Name = {person.Name} playing = {person.Playing}";
    }


}