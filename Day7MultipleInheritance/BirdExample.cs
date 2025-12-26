using System;

// interface 2
interface SingAbility
{

    public void Sing();

}

// interface 1
interface DanceAbility
{
    public void Dance();
}

// class inheriting from multiple interfaces
class Bird : SingAbility, DanceAbility
{

    public string name;
    public string colour;


    // constructor
    public Bird(string name, string colour)
    {
        this.name = name;
        this.colour = colour;
    }

    // defining Function1
    public void Sing()
    {
        Console.WriteLine("The bird sings!");
    }
    
    // defining Function2
    public void Dance()
    {
        Console.WriteLine("The bird dances!");
    }
}

class BirdExample
{
    public static void Main(string[] args)
    {

        // passing parameters into the constructor
        Bird b1 = new Bird("birdy", "white");

        b1.Dance();
        b1.Sing();

        Console.WriteLine("Bird's name is: " + b1.name);
        Console.WriteLine("Bird's colour is: " + b1.colour);
    }
}

