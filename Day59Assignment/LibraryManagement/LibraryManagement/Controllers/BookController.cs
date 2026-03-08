using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        public IActionResult List()
        {
            var books = _repo.GetAllBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _repo.GetBookById(id);
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _repo.AddBook(book);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            _repo.DeleteBook(id);
            return RedirectToAction("List");
        }
    }
}