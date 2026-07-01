using КУРСАЧ.Models;
using КУРСАЧ.Repositories;

namespace КУРСАЧ.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;

        public BookService(
            IBookRepository bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Genre> genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetBooksWithAuthorsAndGenresAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author, string? genre, string? status)
        {
            return await _bookRepository.SearchAsync(title, author, genre, status);
        }

        public async Task AddBookAsync(Book book, List<int> authorIds, List<int> genreIds)
        {
            // Загружаем авторов и жанры по ID
            var authors = await _authorRepository.FindAsync(a => authorIds.Contains(a.Id));
            var genres = await _genreRepository.FindAsync(g => genreIds.Contains(g.Id));

            book.Authors = authors.ToList();
            book.Genres = genres.ToList();

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAsync();
        }

        public async Task UpdateBookAsync(Book book, List<int> authorIds, List<int> genreIds)
        {
            // Загружаем авторов и жанры по ID
            var authors = await _authorRepository.FindAsync(a => authorIds.Contains(a.Id));
            var genres = await _genreRepository.FindAsync(g => genreIds.Contains(g.Id));

            book.Authors = authors.ToList();
            book.Genres = genres.ToList();

            _bookRepository.Update(book);
            await _bookRepository.SaveAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _bookRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<string>> GetAllStatusesAsync()
        {
            // Возвращаем список возможных статусов
            return await Task.FromResult(new List<string>
            {
                "В наличии",
                "Выдана",
                "В ремонте"
            });
        }
    }
}