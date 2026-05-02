using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Services;

public interface IEmailService
{
    Task SendOtpEmailAsync(string toEmail, string otp);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendOtpEmailAsync(string toEmail, string otp)
    {
        var host = _config["SmtpSettings:Host"] ?? "localhost";
        var portStr = _config["SmtpSettings:Port"] ?? "1025";
        int.TryParse(portStr, out int port);
        
        var username = _config["SmtpSettings:Username"];
        var password = _config["SmtpSettings:Password"];
        var fromEmail = _config["SmtpSettings:FromEmail"] ?? "noreply@shopsphere.local";

        using var client = new SmtpClient(host, port);
        
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = true;
        }

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail, "ShopSphere Security"),
            Subject = "Your Login Verification Code",
            Body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h2 style='color: #00C9A7;'>Login Verification</h2>
                    <p>Hello,</p>
                    <p>Your one-time password (OTP) to securely sign in is:</p>
                    <div style='background-color: #f4f4f4; padding: 15px; text-align: center; font-size: 24px; font-weight: bold; letter-spacing: 5px; border-radius: 4px; margin: 20px 0;'>
                        {otp}
                    </div>
                    <p>This code will expire in <strong>5 minutes</strong>.</p>
                    <p style='color: #777; font-size: 12px; margin-top: 40px;'>If you did not request this code, please ignore this email.</p>
                </div>
            ",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(toEmail);

        try
        {
            await client.SendMailAsync(mailMessage);
            Console.WriteLine($"[EmailService] Sent OTP email to {toEmail}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EmailService] ERROR: Failed to send email to {toEmail}. {ex.Message}");
            // We catch the error so it doesn't crash the login flow if SMTP isn't configured yet
        }
    }
}
