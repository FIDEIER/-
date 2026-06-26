using КУРСАЧ.Models;

namespace КУРСАЧ.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> SearchAsync(string? title, string? author, string? genre, string? status);
        Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync();
        Task<Book?> GetBookWithDetailsAsync(int id);
    }
}