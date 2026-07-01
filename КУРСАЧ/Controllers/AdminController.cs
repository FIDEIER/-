using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using КУРСАЧ.Models;
using КУРСАЧ.Repositories;
using КУРСАЧ.Services;

namespace КУРСАЧ.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;

        public AdminController(
            IBookService bookService,
            IRepository<Author> authorRepository,
            IRepository<Genre> genreRepository)
        {
            _bookService = bookService;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        // Проверка авторизации
        private bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

        // Панель управления
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        // Добавление книги (GET)
        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            ViewBag.Authors = await _authorRepository.GetAllAsync();
            ViewBag.Genres = await _genreRepository.GetAllAsync();
            return View(new Book());
        }

        // Добавление книги (POST)
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book, int[] selectedAuthors, int[] selectedGenres)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorRepository.GetAllAsync();
                ViewBag.Genres = await _genreRepository.GetAllAsync();
                return View(book);
            }

            await _bookService.AddBookAsync(book, selectedAuthors.ToList(), selectedGenres.ToList());
            return RedirectToAction("Index");
        }

        // Редактирование книги (GET)
        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            ViewBag.Authors = await _authorRepository.GetAllAsync();
            ViewBag.Genres = await _genreRepository.GetAllAsync();
            ViewBag.SelectedAuthors = book.Authors.Select(a => a.Id).ToArray();
            ViewBag.SelectedGenres = book.Genres.Select(g => g.Id).ToArray();
            return View(book);
        }

        // Редактирование книги (POST)
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book, int[] selectedAuthors, int[] selectedGenres)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorRepository.GetAllAsync();
                ViewBag.Genres = await _genreRepository.GetAllAsync();
                ViewBag.SelectedAuthors = selectedAuthors ?? Array.Empty<int>();
                ViewBag.SelectedGenres = selectedGenres ?? Array.Empty<int>();
                return View(book);
            }

            await _bookService.UpdateBookAsync(book, selectedAuthors.ToList(), selectedGenres.ToList());
            return RedirectToAction("Index");
        }

        // Удаление книги
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login", "Account");

            await _bookService.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }
    }
}