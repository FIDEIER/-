using КУРСАЧ.Models;
using КУРСАЧ.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace КУРСАЧ.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> AuthenticateAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            if (user == null)
                return null;

            // Проверка пароля (хэширование)
            var hashedPassword = HashPassword(password);
            if (user.PasswordHash != hashedPassword)
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}