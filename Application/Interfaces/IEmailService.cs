using Domain.Entities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendNewPasswordEmail(User user, string newPassword);
        Task SendPasswordResetEmail(User user, string token);
        Task SendEmail(MimeMessage message);
    }
}
