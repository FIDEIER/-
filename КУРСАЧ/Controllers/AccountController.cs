using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using КУРСАЧ.Services;

namespace КУРСАЧ.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // Страница входа
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Обработка входа
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Введите логин и пароль.";
                return View();
            }

            var user = await _authService.AuthenticateAsync(login, password);
            if (user == null)
            {
                ViewBag.Error = "Неверный логин или пароль.";
                return View();
            }

            // Сохраняем сессию (простой способ — через HttpContext.Session)
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Admin");
        }

        // Выход
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}