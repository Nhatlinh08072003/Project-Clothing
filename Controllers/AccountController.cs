// Update your AccountController with all necessary using directives
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Clothing.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Project_Clothing.Controllers;

public class AccountController : Controller
{        private readonly ILogger<AccountController> _logger;
    private readonly WeatheredContext _context;
    public AccountController(ILogger<AccountController> logger, WeatheredContext context)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IActionResult Login()
    {
        return View();
    }

   
      [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

     [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterDTO model)
    {
        try
        {
            if (model == null)
            {
                _logger.LogError("Register model is null");
                return View(new RegisterDTO());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if email already exists
            if (_context.Users == null)
            {
                _logger.LogError("Users DbSet is null");
                ModelState.AddModelError("", "System error occurred. Please try again later.");
                return View(model);
            }

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already registered");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                Password = HashPassword(model.Password),
                Username = model.Email,
                CreatedAt = DateTime.UtcNow,
                Status = true,
                Role = 2
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            ModelState.AddModelError("", "An error occurred during registration. Please try again.");
            return View(model);
        }
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
      public IActionResult ForgotPassword()
    {
        return View();
    }
        public IActionResult ChangePassword()
    {
        return View();
    }
   public IActionResult ProfileUser()
    {
        return View();
    }
    
   public IActionResult PurchaseHistory()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
