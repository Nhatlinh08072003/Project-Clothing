using Microsoft.AspNetCore.Mvc;

public class EmailController : Controller
{
    private readonly EmailService _emailService;

    public EmailController()
    {
        _emailService = new EmailService(); // Tạo instance của EmailService
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(string to, string subject, string body)
    {
        try
        {
            await _emailService.SendEmailAsync(to, subject, body);
            return Ok("Email sent successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to send email: {ex.Message}");
        }
    }
}
