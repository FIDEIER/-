using Microsoft.EntityFrameworkCore;
using КУРСАЧ.Data;
using КУРСАЧ.Models;

namespace КУРСАЧ.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByLoginAsync(string login);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDBContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}