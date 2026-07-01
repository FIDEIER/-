using КУРСАЧ.Models;

namespace КУРСАЧ.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author, string? genre, string? status);
        Task AddBookAsync(Book book, List<int> authorIds, List<int> genreIds);
        Task UpdateBookAsync(Book book, List<int> authorIds, List<int> genreIds);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<string>> GetAllStatusesAsync();
    }
}