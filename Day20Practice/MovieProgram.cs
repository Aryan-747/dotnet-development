public class Movie
{
    // Required Variables
    public string?  Title;
    public string? Artist;
    public string? Genre;
    public int Ratings;

    // Default & Paramterized Constructor

    public Movie(){}
    public Movie(string title, string artist, string genre, int ratings)
    {
        Title = title;
        Artist = artist;
        Genre = genre;
        Ratings = ratings;
    }
}

public class MovieProgram
{   

    // Initializing a PreConfigured List Of Movies
    public static List<Movie> MovieList = new List<Movie>()
    {
        new Movie("Avengers","Marvel","Sci-Fi",9),
        new Movie("Iron Man","Marvel","Sci-Fi",10),
        new Movie("Conjuring","Warner Bros","Horror",9),
        new Movie("Dhoom","YJR","Action",8),
    };

    public static List<Movie> AddMovie(string input)
    {
        string[] inp = input.Split(','); // splitted input

        if(inp.Count()<4)
        {
            Console.WriteLine("Inavlid Input!");
            return MovieList;
        }

        Movie m1 = new Movie(inp[0],inp[1],inp[2],int.Parse(inp[3]));
        MovieList.Add(m1);

        return MovieList;
    }

    // Fetching Movies By Genre & returning
    public static List<Movie> ViewMoviesByGenre(string genre)
    {
        List<Movie> resultant = new List<Movie>();

        foreach(var item in MovieList)
        {   
            // Adding Movie Object in new List if Genre is Same
            if(item.Genre.Equals(genre,StringComparison.OrdinalIgnoreCase))
            {
                resultant.Add(item);
            }
        }

        return resultant;
    }

    // Function to Sort Movies In Ascending Order of Their Ratings
    public static List<Movie> ViewMoviesByRatings()
    {
        return MovieList.OrderBy(x => x.Ratings).ToList(); // returning sorted List
    }

    public static void Main(string[] args)
    {
        // Initial Movie List
        Console.WriteLine("---Initial Movie List---");
        foreach(Movie m1 in MovieList)
        {
            Console.WriteLine($"{m1.Title}, {m1.Artist}, {m1.Genre}, {m1.Ratings}");
        }
        Console.WriteLine();

        // Taking Input
        Console.WriteLine("---Enter the details of movie you want to enter in provided format---");
        Console.Write("Title,Artist,Genre,Rating: ");
        string inp = Console.ReadLine();
        MovieList = AddMovie(inp);
        // Displaying Movie Details
        Console.WriteLine("\n---Updated List After Movie Addition---");
        foreach(var items in MovieList)
        {
            Console.WriteLine($"{items.Title},{items.Artist},{items.Genre},{items.Ratings}");
        }
        Console.WriteLine();

        // Displaying Movies With Inputted Genre
        Console.Write("Enter the Genre: ");
        string genre = Console.ReadLine();
        List<Movie> Mlist = ViewMoviesByGenre(genre);
        // None Found in Genre
        if(Mlist.Count() == 0)
        {
            Console.WriteLine($"No Movies found in Genre: {genre}");
        }
        else
        {
            foreach(var item in Mlist)
            {
                Console.WriteLine($"{item.Title}, {item.Artist}, {item.Genre}, {item.Ratings}");
            }
        }
        Console.WriteLine();

        // Sorting Movies By Ratings & Returning new List
        Console.WriteLine("---The Sorted List Of Movies According To Ratings---");

        List<Movie> newList = ViewMoviesByRatings();
        foreach(var item in newList)
        {
            Console.WriteLine($"{item.Title}, {item.Artist}, {item.Genre}, {item.Ratings}");
        }
        Console.WriteLine();

    }
        
}

