using Application.DataTransfer;
using Application.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _emailFrom;
        private readonly string _emailPassword;

        public SmtpEmailSender(string emailFrom, string emailPassword)
        {
            _emailFrom = emailFrom;
            _emailPassword = emailPassword;
        }

        public void Send(SendEmailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailFrom, _emailPassword)
            };

            var message = new MailMessage(_emailFrom, dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
