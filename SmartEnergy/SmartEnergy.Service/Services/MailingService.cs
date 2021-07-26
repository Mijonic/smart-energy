using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class MailingService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string to, string subject, string text)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("SmartEnergy team", _configuration["Smtp:Username"]));
            mailMessage.To.Add(new MailboxAddress("You", to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), true);
                smtpClient.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
