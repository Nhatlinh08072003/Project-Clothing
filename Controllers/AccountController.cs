using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

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
                var hashedPassword = model.Password;
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

        // Password hashing function
        // private string HashPassword(string password)
        // {
        //     using (var sha256 = SHA256.Create())
        //     {
        //         var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //         return Convert.ToBase64String(hashedBytes);
        //     }
        // }
// GET: Reset Password
public IActionResult ResetPassword()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
[Authorize]
public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var userId = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        return RedirectToAction("Login");
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == int.Parse(userId));
    if (user == null)
    {
        return NotFound();
    }

    // Add logging
    var hashedCurrentPassword =model.CurrentPassword;
    _logger.LogInformation($"Current Password Hash: {hashedCurrentPassword}");
    _logger.LogInformation($"Stored Password Hash: {user.Password}");

    // Verify current password
    if (user.Password != hashedCurrentPassword)
    {
        ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng");
        return View(model);
    }

    try
    {
        var newHashedPassword = model.NewPassword;
        _logger.LogInformation($"New Password Hash: {newHashedPassword}");

        // Update password
        user.Password = newHashedPassword;
        
        // Try both approaches
        _context.Users.Update(user);
        // AND
        _context.Entry(user).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

        // Sign out user
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công. Vui lòng đăng nhập lại.";
        return RedirectToAction("Login");
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error updating password: {ex.Message}");
        ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật mật khẩu");
        return View(model);
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



[Authorize] // Ensure only logged-in users can access
public async Task<IActionResult> Profile()
{
    var userId = User.FindFirst("UserId")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        return RedirectToAction("Login");
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == int.Parse(userId));
    if (user == null)
    {
        return NotFound();
    }

    // Create the UserProfileDTO to pass to the view
    var model = new UserProfileDTO
    {
        Username = user.Username,
        Email = user.Email,
        Phone = user.Phone,
        Address = user.Address,
        LastLogin = user.LastLogin
    };

    // Return the view with the model
    return View(model);
}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
