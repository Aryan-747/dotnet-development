using System;
class Palindrome
{
     // Function to Check Whether Input String is Palindrome.
    static bool IsPalindrome(string sentence)
    {
        string senreverse = "";
        // storing the reversed string in a new variable
        for(int i=sentence.Length-1; i>=0 ; i--)
        {
            senreverse += sentence[i];
        }

        // comparing the reversed string and input string and returning the result;

        if(senreverse == sentence)
        {
            return true; // is Palindrome
        }

        return false; // is Not a Palindrome
    }
    public static void Main(string[] args)
    {
        string sentence; // Input sentence that has to be checked!
        Console.WriteLine("Enter the sentence you want to check: ");
        sentence = Console.ReadLine();


        // calling function to check palindrome and displaying whether it is a palindrome or not
        Console.WriteLine($"{sentence} is a palindrome: " + IsPalindrome(sentence));        
    }
};