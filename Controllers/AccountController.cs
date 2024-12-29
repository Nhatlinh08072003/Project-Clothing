using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;

namespace Project_Clothing.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly WeatheredContext _context;

        public AccountController(ILogger<AccountController> logger, WeatheredContext context)
        {
            _logger = logger;
            _context = context;
        }

        // View for Login
        public IActionResult Login()
        {
            return View();
        }

        // POST action for Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);

                if (user != null)
                {
                    // Cập nhật thời gian đăng nhập
                    user.LastLogin = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    // Tạo claims cho user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role?.ToString() ?? "2"),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("Phone", user.Phone?.ToString() ?? "")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    _logger.LogInformation($"User {user.Email} logged in successfully");
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
            }

            return View(model);
        }

     

        // POST action for Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất khỏi hệ thống
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Chuyển hướng về trang chủ hoặc trang khác
            return RedirectToAction("Index", "Home");
        }
 public IActionResult Resetpassword()
        {
            return View();
        }
        // Password hashing function
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }


 public IActionResult ForgotPassword()
    {
        return View();
    }
 public IActionResult  PurchaseHistory()
    {
        return View();
    }


 public IActionResult Profile()
    {
        return View();
    }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
