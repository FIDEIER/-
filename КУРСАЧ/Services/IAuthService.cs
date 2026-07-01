using КУРСАЧ.Models;

namespace КУРСАЧ.Services
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string login, string password);
    }
}