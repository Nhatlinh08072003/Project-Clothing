using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class EmailService
{
    private readonly string smtpServer = "smtp.gmail.com"; // Máy chủ SMTP của Gmail
    private readonly int port = 587; // Cổng SMTP (TLS)
    private readonly string senderEmail = "letiennhatlinh08072003@gmail.com"; // Địa chỉ email gửi
    private readonly string password = "fyjwdatfhtfinfmm"; // Mật khẩu ứng dụng của Gmail

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();

        // Cập nhật cách thêm địa chỉ người gửi và người nhận
        message.From.Add(new MailboxAddress("Sender Name", senderEmail)); // Thêm tên người gửi
        message.To.Add(new MailboxAddress("", to)); // Thêm địa chỉ người nhận (dùng "" cho tên người nhận)

        message.Subject = subject; // Tiêu đề email

        // Tạo nội dung email
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body // Nội dung email dạng HTML
        };
        message.Body = bodyBuilder.ToMessageBody();

        // Kết nối tới SMTP server và gửi email
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls); // Kết nối với bảo mật TLS
            await client.AuthenticateAsync(senderEmail, password); // Xác thực
            await client.SendAsync(message); // Gửi email
        }
        finally
        {
            await client.DisconnectAsync(true); // Ngắt kết nối
        }
    }
}
