using Microsoft.EntityFrameworkCore;
using КУРСАЧ.Data;
using КУРСАЧ.Models;

namespace КУРСАЧ.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> SearchAsync(string? title, string? author, string? genre, string? status)
        {
            var query = _dbSet
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Authors.Any(a => a.Name.Contains(author)));

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genres.Any(g => g.Name.Contains(genre)));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(b => b.Status == status);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorsAndGenresAsync()
        {
            return await _dbSet
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ToListAsync();
        }

        public async Task<Book?> GetBookWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}