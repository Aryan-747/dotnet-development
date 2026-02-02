class Book
 {
    public int Id;
    public string Title;
    public string Author;
    public string Genre;
    public int PublicationYear;

    // Parameterized Constructor
     public Book(int id,string title, string author, string genre ,int publicationyear)
     {
         Id = id;
         Title = title;
         Author = author;
         Genre = genre;
         PublicationYear = publicationyear;
     }
}

class LibraryUtility
{
    // List to Store Books
    private SortedDictionary<int,Book> BookList = new SortedDictionary<int,Book>();

    // Method 1: Adds Book details in Dictionary (Auto Increments ID)
    private int id = 1;
    public void AddBook(string title, string author, string genre, int year)
    {
        BookList.Add(id,new Book(id,title,author,genre,year));
        id++;
    }

    // Method 2: Returns a List of Books By Author
    public List<Book> GetBooksByAuthor(string author)
    {
        List<Book> result = new List<Book>();
        foreach(Book x in BookList.Values)
        {
            if(x.Author.Equals(author,StringComparison.OrdinalIgnoreCase))
            {
                result.Add(x);
            }
        }
        return result;
    }
    
    // Method 3: Group Books By Genre

    public SortedDictionary<string,List<Book>> GroupBooksByGenre()
    {
        SortedDictionary<string, List<Book>> ByGenre = new SortedDictionary<string, List<Book>>();

        foreach(Book x in BookList.Values)
        {
            string genre = x.Genre;

            if(ByGenre.ContainsKey(genre))
            {
                ByGenre[genre].Add(x);
            }

            else
            {
                List<Book> b1 = new List<Book>();
                b1.Add(x);
                ByGenre.Add(genre, b1);
            }
        }
        return ByGenre;
    }

    // Method 4: Display Book Details
    public void DisplayBooks()
    {
        Console.WriteLine("---Book Details---");

        foreach(Book x in BookList.Values)
        {
            Console.WriteLine($"Id: {x.Id}");
            Console.WriteLine($"Title: {x.Title}");
            Console.WriteLine($"Author: {x.Author}");
            Console.WriteLine($"Genre: {x.Genre}");
            Console.WriteLine($"Publication Year: {x.PublicationYear}");
            Console.WriteLine("--------------------------------------");
        }
    }
    
    // Method 5: Returns Total Number Of Books In List
    public int GetTotalBooksCount()
    {
        return BookList.Count();
    }
}

class Program
{
    public static void Main(string[] args)
    {
        LibraryUtility obj = new LibraryUtility();

        obj.AddBook("GOT","Ron","Thriller",2024);
        obj.AddBook("AWS","Bezos","Tech",2018);
        obj.AddBook("Space", "Bezos", "Science", 2015);
        obj.AddBook("Nuke", "Robert", "Science", 1992);

        obj.DisplayBooks();

        Console.WriteLine("Total Number of Books in List: " + obj.GetTotalBooksCount());

        string author = "bezos";

        List<Book> Fin = obj.GetBooksByAuthor(author);

        Console.WriteLine($"Books By Author {author}:");
        foreach(var x in Fin)
        {
            Console.WriteLine(x.Title);
        }

        // 2. Display books grouped by genre
        Console.WriteLine("Books Grouped By Genre:");
        var groupedBooks = obj.GroupBooksByGenre();

        foreach (var genre in groupedBooks)
        {
            Console.WriteLine("\nGenre: " + genre.Key);
            foreach (var book in genre.Value)
            {
                Console.WriteLine($"{book.Id}|{book.Title}|{book.Author}|{book.Genre}|{book.PublicationYear}");
            }
        }
    }
}