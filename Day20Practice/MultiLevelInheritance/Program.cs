class Parent
{
    public void display1()
    {
        Console.WriteLine("Hello1");
    }
}
class Child1 : Parent
{
    public void display2()
    {
        Console.WriteLine("Hello2");
    }
}
class Child2 : Child1
{
    public void display3()
    {
        Console.WriteLine("Hello3");
    }
}
class Program
{
    public static void Main(string[] args)
    {
        Child2 obj = new Child2();

        obj.display1();
        obj.display2();
        obj.display3();
    }
}