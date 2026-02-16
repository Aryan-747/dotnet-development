public class Book
{
    public string ISBN {get; set;}
    public string Title {get; set;}
    public string Author {get; set;}
    public string Genre {get; set;}
    public bool isAvailable {get; set;}

}

public class Catalog<T> where T : Book
{
    private List<T> _items = new List<T>();
    private HashSet<string> _isbnSet = new HashSet<string>();
    private SortedDictionary<string,List<T>> _genreIndex = new SortedDictionary<string,List<T>>();
    
    string genre;
    public bool AddItem(T item)
    {

        genre = item.Genre;

        if(_isbnSet.Contains(item.ISBN))
        {
            return false; // Already In Set
        }

        if(_genreIndex.ContainsKey(genre))
        {
            _genreIndex[genre].Add(item);
            _items.Add(item);
            _isbnSet.Add(item.ISBN);
            return true;
        }

        else
        {
            List<T> books = new List<T>();
            books.Add(item);
            _genreIndex.Add(genre,books);
            _items.Add(item);
            _isbnSet.Add(item.ISBN);
            return true;
        }
    }

    // Get Books By Genre using Indexer
    public List<T> this[string genre]
    {
        get
        {
            if(_genreIndex.ContainsKey(genre))
            {
                return _genreIndex[genre];
            }

            List<T> res = new List<T>();

            return res; // returns empty list if genre is not found
        }
    }

    // find books using LINQ and Lambda Expressions
    public IEnumerable<T> FindBooks(Func<T,bool> predicate)
    {
        return _items.Where(predicate);
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Catalog<Book> library = new Catalog<Book>();

    Book book1 = new Book 
    { 
        ISBN = "978-3-16-148410-0", 
        Title = "C# Programming", 
        Author = "John Sharp", 
        Genre = "Programming" 
    };

    Book book2 = new Book 
    { 
        ISBN = "979-3-16-148410-0", 
        Title = "F# Programming", 
        Author = "Central Cee Sharp", 
        Genre = "Programming" 
    };

    Book book3 = new Book 
    { 
        ISBN = "911-3-16-148410-0", 
        Title = "Life", 
        Author = "Joao Neves", 
        Genre = "Psychology" 
    };

    library.AddItem(book1);
    library.AddItem(book2);
    library.AddItem(book3);

    var programmingBooks = library["Psychology"];
    Console.WriteLine(programmingBooks.Count); // Should output: 1

    var johnsBooks = library.FindBooks(b => b.Author.Contains("Central"));
    Console.WriteLine(johnsBooks.Count()); // Should output: 1

    }
}