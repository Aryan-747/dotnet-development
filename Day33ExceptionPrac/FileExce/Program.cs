using System;

class Program
{
    public static void Main(string[] args)
    {
        string filepath = "data.txt";

        try
        {
            // Reading file content
            using(StreamReader reader = new StreamReader(filepath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("File Content:");
                Console.WriteLine(content);
            }
        }

        catch(FileNotFoundException)
        {
            Console.WriteLine("Error: the file was not found!");
        }

        catch(UnauthorizedAccessException)
        {
            Console.WriteLine("Error: you do not have access to the requested file");
        }

        // Catches any other exception
        catch(Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        finally
        {
            // this always executes
            Console.WriteLine("File Operation Completed!");
        }
    }
}