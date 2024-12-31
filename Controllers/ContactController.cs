using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly EmailService _emailService;  // Inject EmailService

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
            _emailService = new EmailService();  // Create EmailService instance
        }

        public IActionResult Lienhe()
        {
            return View();
        }
[HttpPost]
public async Task<IActionResult> SubmitContact(string name, string email, string message)
{
    try
    {
        // Ensure recipient email is correct and valid
        string recipientEmail = "letiennhatlinh08072003@gmail.com"; // Replace with a valid email address

        var subject = "Tin nhắn từ " + name;
        var body = $"<h2>Thông tin người gửi</h2>" +
                    $"<p><strong>Tên:</strong> {name}</p>" +
                    $"<p><strong>Email:</strong> {email}</p>" +
                    $"<h3>Nội dung tin nhắn:</h3>" +
                    $"<p>{message}</p>";

        // Send email to the recipient (admin, support, etc.)
        await _emailService.SendEmailAsync(recipientEmail, subject, body);

        // Send confirmation email to the user
        var userSubject = "Xác nhận nhận tin nhắn";
        var userBody = $"<h2>Chúng tôi đã nhận được tin nhắn của bạn!</h2>" +
                       $"<p>Cảm ơn {name} đã liên hệ với chúng tôi. Chúng tôi sẽ trả lời bạn sớm nhất có thể.</p>" +
                       $"<p><strong>Email của bạn:</strong> {email}</p>" +
                       $"<p><strong>Nội dung tin nhắn:</strong><br>{message}</p>";

        // Send confirmation email to the user
        await _emailService.SendEmailAsync(email, userSubject, userBody);

       TempData["SuccessMessage"] = "Tin nhắn của bạn đã được gửi thành công!";  
return RedirectToAction("Lienhe");
    }
    catch (Exception ex)
    {
        // Log the exception for better debugging
        _logger.LogError(ex, "Error sending email.");

        // Handle errors
        ViewData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
        return View("Lienhe");
    }
}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
