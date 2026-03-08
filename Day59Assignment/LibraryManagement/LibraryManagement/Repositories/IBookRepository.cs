using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();

        Book GetBookById(int id);

        void AddBook(Book book);

        void DeleteBook(int id);
    }
}