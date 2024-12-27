using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Project_Clothing.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly WeatheredContext _context; // Replace with your actual DbContext name

        public RegisterController(ILogger<RegisterController> logger, WeatheredContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Register/Dangki
        public IActionResult Dangki()
        {
            return View();
        }

        // POST: Register/Dangki
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dangki(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(model);
                }

                // Create new user
                var user = new User
{
    Username = model.Email, // Thêm dòng này vì Username là required
    Email = model.Email,
    Phone = model.Phone,
    Address = model.Address,
    Password = HashPassword(model.Password),
    CreatedAt = DateTime.UtcNow,
    Status = true,
    Role = 2
};
try
{
    _context.Users.Add(user);
    _logger.LogInformation($"Attempting to save user: {user.Email}");
    await _context.SaveChangesAsync();
    _logger.LogInformation($"Successfully saved user: {user.Email}");
    
    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
    return RedirectToAction("Dangnhap", "Login");
}
catch (Exception ex)
{
    _logger.LogError($"Error during registration. Exception: {ex.Message}");
    _logger.LogError($"Stack trace: {ex.StackTrace}");
    if (ex.InnerException != null)
    {
        _logger.LogError($"Inner exception: {ex.InnerException.Message}");
    }
    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình đăng ký. Vui lòng thử lại sau.");
}
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}