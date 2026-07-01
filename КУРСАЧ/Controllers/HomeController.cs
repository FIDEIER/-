using Microsoft.AspNetCore.Mvc;
using КУРСАЧ.Services;

namespace КУРСАЧ.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string? searchTitle, string? searchAuthor, string? searchGenre, string? searchStatus)
        {
            var books = await _bookService.SearchBooksAsync(searchTitle, searchAuthor, searchGenre, searchStatus);
            ViewBag.SearchTitle = searchTitle;
            ViewBag.SearchAuthor = searchAuthor;
            ViewBag.SearchGenre = searchGenre;
            ViewBag.SearchStatus = searchStatus;
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            return View(book);
        }
    }
}