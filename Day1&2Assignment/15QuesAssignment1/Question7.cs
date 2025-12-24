using System;

class VowelOrNot
{

    char charactertocheck;

    public VowelOrNot(char character)
    {
        this.charactertocheck = character;
    }

    public bool Check()
    {
      switch(charactertocheck)
        {
            case 'a':
                return true;
            case 'A':
                return true;
            case 'e':
                return true;
            case 'E':
                return true;
            case 'i':
                return true;
            case 'I':
                return true;
            case 'o':
                return true;
            case 'O':
                return true;
            case 'u':
                return true;
            case 'U':
                return true;
            default:
                return false;
         }
    }
}


class Question7
{
    public static void Main(string[] args)
    {
        // Taking Input
        string input;
        Console.Write("Enter the character: ");
        input = Console.ReadLine();

        if (char.TryParse(input, out char character))
        {

            VowelOrNot v1 = new VowelOrNot(character);
            Console.WriteLine("Entered Character is a vowel: " + v1.Check());
        }
    }
}