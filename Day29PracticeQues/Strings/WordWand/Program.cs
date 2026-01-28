using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Implementation Class
class WordWand
{
    public string sentence;
    public string updatedstring = "";
    public int WordCount = 0;

    // Checks if input string is valid
    public bool isValid()
    {
        for(int i=0 ; i<sentence.Length; i++)
        {   
            // Valid
            if(sentence[i] == ' ' || Char.IsLetter(sentence[i]))
            {
                
            }

            // Invalid Input
            else
            {
                return false;
            }
        }

        return true;
    }

    // reverses input string
    public string reverse(string inp)
    {
        string newstr = "";
        for(int i=inp.Length-1; i>=0 ; i--)
        {
            newstr += inp[i];
        }

        return newstr;
    }


    public void Operation()
    {
        // Converting input to string array for word count
        string[] array = sentence.Split(' ');
        WordCount = array.Length;

        // Even Number of Words
        if(WordCount%2 == 0)
        {
            // Reverse words

            for(int i=WordCount-1 ; i>=0 ; i--)
            {
                // special case to avoid extra whitespace at the end
                if(i!=0)
                {
                    updatedstring += array[i] + " ";
                }

                else
                {
                    updatedstring += array[i];
                }
            }
        }

        // Odd Number of Words
        else
        {
            // Reverse each letter of word
            for(int i=0 ; i<WordCount; i++)
            {   
                // special case to avoid extra whitespace at the end
                if(i!=WordCount-1)
                {
                    updatedstring += reverse(array[i])+ " ";
                }
                else
                {
                    updatedstring += array[i];
                }
            }
            
        }
    }

    public string GetUpdatedString()
    {
        return updatedstring;
    }

    public int GetCount()
    {
        return WordCount;
    }
}

// Main Class
class Program
{
    public static void Main(string[] args)
    {
        string input;
        Console.Write("Enter the sentence: ");
        input = Console.ReadLine();

        // Initializing Object of class
        WordWand obj = new WordWand
        {
            sentence = input,
        };

        if(obj.isValid())
        {
            obj.Operation();
            Console.WriteLine(obj.GetCount());
            Console.WriteLine(obj.GetUpdatedString());
        }

        else
        {
            Console.WriteLine("Invalid Sentence");
        }

    }
}