using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using MailKit.Net.Smtp;


namespace Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNewPasswordEmail(User user, string newPassword)
        {
            try
            {
                var emailBody = $@"
                    <html>
                    <body>
                        <p>Dear {user.Name}, your password has been reset.</p>
                        <p>New Password: {newPassword}</p>
                        <p>Login into the web and change this password for security.</p>
                    </body>
                    </html>";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("CleanArchitectureAPI", _configuration["Smtp:FromEmail"]));
                message.To.Add(new MailboxAddress(user.Name, user.Email));
                message.Subject = "New password generated";
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = emailBody
                };
                message.Body = bodyBuilder.ToMessageBody();

                await SendEmail(message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred: {ex.Message}", ex);
            }
        }

            public async Task SendPasswordResetEmail(User user, string token)
            {
                try
                {
                    var encodedToken = WebUtility.UrlEncode(token);
                    string resetLink = $"https://localhost:7037/api/Authentication/ResetPassword?token={encodedToken}";
                    string clickableLink = $"<a href=\"{resetLink}\">Click here to reset the your account password.</a>";
                    var emailBody = $@"
                        <html>
                        <body>
                            <p>Reseting Password. Please click the link below to reset the passward account:</p>
                            <p>{clickableLink}</p>
                            <p>After click on the link you will receive your new password on your email</p>
                        </body>
                        </html>";

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("CleanArchitectureAPI", _configuration["Smtp:FromEmail"]));
                    message.To.Add(new MailboxAddress(user.Name, user.Email));
                    message.Subject = "Password Reset";
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = emailBody,
                    };
                    message.Body = bodyBuilder.ToMessageBody();

                    await SendEmail(message);
                }
                catch (Exception ex) 
                { 
                    throw new ApplicationException($"An error ocurred: {ex.Message}", ex);
                }
            }

        public async Task SendEmail(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var host = _configuration["Smtp:Host"];
                    var port = int.Parse(_configuration["Smtp:Port"]);
                    var username = _configuration["Smtp:FromEmail"];
                    var password = _configuration["Smtp:Password"];

                    await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(username, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
